namespace LL.MDE.Components.Qvt.Transformation.EA2FMEA
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using LL.MDE.Components.Qvt.Common;
	using LL.MDE.DataModels.EnAr;
	using LL.MDE.DataModels.XML;

	public class RelationProduct2Structure
	{
		private readonly IMetaModelInterface editor;
		private readonly TransformationEA2FMEA transformation;

		private Dictionary<CheckOnlyDomains, EnforceDomains> traceabilityMap = new Dictionary<CheckOnlyDomains, EnforceDomains>();

		public RelationProduct2Structure(IMetaModelInterface editor , TransformationEA2FMEA transformation )
		{
			this.editor = editor;this.transformation = transformation;
		}

		public EnforceDomains FindPreviousResult(LL.MDE.DataModels.EnAr.Package productP,LL.MDE.DataModels.XML.Tag fmStructureRefs,LL.MDE.DataModels.XML.Tag fmStructureElements)
		{
			CheckOnlyDomains input = new CheckOnlyDomains(productP,fmStructureRefs,fmStructureElements);
			return traceabilityMap.ContainsKey(input) ? traceabilityMap[input] : null;
		}

		internal static ISet<CheckResultProduct2Structure> Check( LL.MDE.DataModels.EnAr.Package productP,LL.MDE.DataModels.XML.Tag fmStructureRefs,LL.MDE.DataModels.XML.Tag fmStructureElements )
		{
			ISet<CheckResultProduct2Structure> result = new HashSet<CheckResultProduct2Structure>();ISet<MatchDomainProductP> matchDomainProductPs = CheckDomainProductP(productP);ISet<MatchDomainFmStructureRefs> matchDomainFmStructureRefss = CheckDomainFmStructureRefs(fmStructureRefs);ISet<MatchDomainFmStructureElements> matchDomainFmStructureElementss = CheckDomainFmStructureElements(fmStructureElements);foreach (MatchDomainProductP matchDomainProductP in matchDomainProductPs ) {foreach (MatchDomainFmStructureRefs matchDomainFmStructureRefs in matchDomainFmStructureRefss ) {foreach (MatchDomainFmStructureElements matchDomainFmStructureElements in matchDomainFmStructureElementss ) {string productName = matchDomainProductP.productName;
			LL.MDE.DataModels.EnAr.Element productPE = matchDomainProductP.productPE;
			string productID = matchDomainProductP.productID;
			CheckResultProduct2Structure checkonlysMatch = new CheckResultProduct2Structure () {matchDomainProductP = matchDomainProductP,matchDomainFmStructureRefs = matchDomainFmStructureRefs,matchDomainFmStructureElements = matchDomainFmStructureElements,};
			result.Add(checkonlysMatch);			} // End foreach
						} // End foreach
						} // End foreach
				return result;
		}

		internal static ISet<MatchDomainFmStructureElements> CheckDomainFmStructureElements(LL.MDE.DataModels.XML.Tag fmStructureElements)
		{
			ISet<MatchDomainFmStructureElements> result = new HashSet<MatchDomainFmStructureElements>();
			MatchDomainFmStructureElements match = new MatchDomainFmStructureElements() {
			fmStructureElements = fmStructureElements,
			};
			result.Add(match);

			return result;
		}

		internal static ISet<MatchDomainFmStructureRefs> CheckDomainFmStructureRefs(LL.MDE.DataModels.XML.Tag fmStructureRefs)
		{
			ISet<MatchDomainFmStructureRefs> result = new HashSet<MatchDomainFmStructureRefs>();
			MatchDomainFmStructureRefs match = new MatchDomainFmStructureRefs() {
			fmStructureRefs = fmStructureRefs,
			};
			result.Add(match);

			return result;
		}

		internal static ISet<MatchDomainProductP> CheckDomainProductP(LL.MDE.DataModels.EnAr.Package productP)
		{
			ISet<MatchDomainProductP> result = new HashSet<MatchDomainProductP>();
			string productName = productP.Name;
			LL.MDE.DataModels.EnAr.Element productPE = productP.Element;
			string productID = productPE.ElementGUID;
			if (productPE.ElementPackage() == productP) {
			MatchDomainProductP match = new MatchDomainProductP() {
			productP = productP,
			productName = productName,
			productPE = productPE,
			productID = productID,
			};
			result.Add(match);
			}
			return result;
		}

		internal void CheckAndEnforce(LL.MDE.DataModels.EnAr.Package productP,LL.MDE.DataModels.XML.Tag fmStructureRefs,LL.MDE.DataModels.XML.Tag fmStructureElements,LL.MDE.DataModels.XML.Tag fmStructures )
		{
			CheckOnlyDomains input = new CheckOnlyDomains(productP,fmStructureRefs,fmStructureElements);
			EnforceDomains output = new EnforceDomains(fmStructures);
			if (traceabilityMap.ContainsKey(input) && !traceabilityMap[input].Equals(output))
			{
				throw new Exception("This relation has already been used with different enforced parameters!");
			}
			if (!traceabilityMap.ContainsKey(input))
			{
				ISet<CheckResultProduct2Structure> result = Check (productP,fmStructureRefs,fmStructureElements);
				Enforce(result, fmStructures);
				traceabilityMap[input] = output;
			}
		}

		internal void Enforce(ISet<CheckResultProduct2Structure> result, LL.MDE.DataModels.XML.Tag fmStructures )
		{
			foreach (CheckResultProduct2Structure match in result)
			{
			// Extracting variables binded in source domains
			LL.MDE.DataModels.EnAr.Package productP = match.matchDomainProductP.productP;
			string productName = match.matchDomainProductP.productName;
			LL.MDE.DataModels.EnAr.Element productPE = match.matchDomainProductP.productPE;
			string productID = match.matchDomainProductP.productID;
			LL.MDE.DataModels.XML.Tag fmStructureRefs = match.matchDomainFmStructureRefs.fmStructureRefs;
			LL.MDE.DataModels.XML.Tag fmStructureElements = match.matchDomainFmStructureElements.fmStructureElements;

			// Assigning variables binded in the where clause

			// Enforcing each enforced domain
			MatchDomainFmStructures targetMatchDomainFmStructures = EnforceFmStructures(productID,productName,  fmStructures );

			// Retrieving variables binded in the enforced domains
			LL.MDE.DataModels.XML.Tag fmStructure = targetMatchDomainFmStructures.fmStructure;
			LL.MDE.DataModels.XML.Attribute structureId = targetMatchDomainFmStructures.structureId;
			LL.MDE.DataModels.XML.Tag longName1 = targetMatchDomainFmStructures.longName1;
			LL.MDE.DataModels.XML.Tag l41 = targetMatchDomainFmStructures.l41;
			LL.MDE.DataModels.XML.Attribute lAttr1 = targetMatchDomainFmStructures.lAttr1;

			// Calling other relations as defined in the where clause
			new RelationCreateProjectStructureLink(editor,transformation).CheckAndEnforce(structureId,fmStructureRefs);new RelationAddStructureRoot(editor,transformation).CheckAndEnforce(productP,fmStructure,fmStructureElements);}
		}

		internal MatchDomainFmStructures EnforceFmStructures(string productID,string productName, LL.MDE.DataModels.XML.Tag fmStructures)
		{
			MatchDomainFmStructures match = new MatchDomainFmStructures();

			// Contructing fmStructures
			LL.MDE.DataModels.XML.Tag fmStructure = null;
			fmStructure =  (LL.MDE.DataModels.XML.Tag) editor.CreateNewObjectInField(fmStructures, "childTags");

			// Contructing fmStructure
			editor.AddOrSetInField(fmStructure, "tagname", "FM-STRUCTURE" );
			LL.MDE.DataModels.XML.Attribute structureId = null;
			structureId =  (LL.MDE.DataModels.XML.Attribute) editor.CreateNewObjectInField(fmStructure, "attributes");

			LL.MDE.DataModels.XML.Tag longName1 = null;
			longName1 =  (LL.MDE.DataModels.XML.Tag) editor.CreateNewObjectInField(fmStructure, "childTags");

			// Contructing structureId
			editor.AddOrSetInField(structureId, "name", "ID" );
			editor.AddOrSetInField(structureId, "value", productID );

			// Contructing longName1
			editor.AddOrSetInField(longName1, "tagname", "LONG-NAME" );
			LL.MDE.DataModels.XML.Tag l41 = null;
			l41 =  (LL.MDE.DataModels.XML.Tag) editor.CreateNewObjectInField(longName1, "childTags");

			// Contructing l41
			editor.AddOrSetInField(l41, "tagname", "L-4" );
			editor.AddOrSetInField(l41, "value", productName );
			LL.MDE.DataModels.XML.Attribute lAttr1 = null;
			lAttr1 =  (LL.MDE.DataModels.XML.Attribute) editor.CreateNewObjectInField(l41, "attributes");

			// Contructing lAttr1
			editor.AddOrSetInField(lAttr1, "name", "L" );
			editor.AddOrSetInField(lAttr1, "value", "Product2Structure" );

				// Return newly binded variables
								match.fmStructures  = fmStructures;
								match.fmStructure  = fmStructure;
								match.structureId  = structureId;
								match.longName1  = longName1;
								match.l41  = l41;
								match.lAttr1  = lAttr1;
				return match;
		}

		public class CheckOnlyDomains : Tuple<Package,Tag,Tag>
		{
			public CheckOnlyDomains(Package productP,Tag fmStructureRefs,Tag fmStructureElements)
				: base(productP,fmStructureRefs,fmStructureElements)
			{
			}

			public Tag fmStructureElements
			{
				get { return Item3; }
			}

			public Tag fmStructureRefs
			{
				get { return Item2; }
			}

			public Package productP
			{
				get { return Item1; }
			}
		}

		public class EnforceDomains : Tuple<Tag>
		{
			public EnforceDomains(Tag fmStructures)
				: base(fmStructures)
			{
			}

			public Tag fmStructures
			{
				get { return Item1; }
			}
		}

		internal class CheckResultProduct2Structure
		{
			public MatchDomainFmStructureElements matchDomainFmStructureElements;
			public MatchDomainFmStructureRefs matchDomainFmStructureRefs;
			public MatchDomainProductP matchDomainProductP;
		}

		internal class MatchDomainFmStructureElements
		{
			public LL.MDE.DataModels.XML.Tag fmStructureElements;
		}

		internal class MatchDomainFmStructureRefs
		{
			public LL.MDE.DataModels.XML.Tag fmStructureRefs;
		}

		internal class MatchDomainFmStructures
		{
			public LL.MDE.DataModels.XML.Tag fmStructure;
			public LL.MDE.DataModels.XML.Tag fmStructures;
			public LL.MDE.DataModels.XML.Tag l41;
			public LL.MDE.DataModels.XML.Attribute lAttr1;
			public LL.MDE.DataModels.XML.Tag longName1;
			public LL.MDE.DataModels.XML.Attribute structureId;
		}

		internal class MatchDomainProductP
		{
			public string productID;
			public string productName;
			public LL.MDE.DataModels.EnAr.Package productP;
			public LL.MDE.DataModels.EnAr.Element productPE;
		}
	}
}