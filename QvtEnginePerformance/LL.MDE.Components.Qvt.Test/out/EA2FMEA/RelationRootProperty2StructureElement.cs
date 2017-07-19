namespace LL.MDE.Components.Qvt.Transformation.EA2FMEA
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using LL.MDE.Components.Qvt.Common;
	using LL.MDE.DataModels.EnAr;
	using LL.MDE.DataModels.XML;

	public class RelationRootProperty2StructureElement
	{
		private readonly IMetaModelInterface editor;
		private readonly TransformationEA2FMEA transformation;

		private Dictionary<CheckOnlyDomains, EnforceDomains> traceabilityMap = new Dictionary<CheckOnlyDomains, EnforceDomains>();

		public RelationRootProperty2StructureElement(IMetaModelInterface editor , TransformationEA2FMEA transformation )
		{
			this.editor = editor;this.transformation = transformation;
		}

		public EnforceDomains FindPreviousResult(LL.MDE.DataModels.EnAr.Package abstractionLevelP,LL.MDE.DataModels.XML.Tag parentSeDecomposition,LL.MDE.DataModels.XML.Tag stucture)
		{
			CheckOnlyDomains input = new CheckOnlyDomains(abstractionLevelP,parentSeDecomposition,stucture);
			return traceabilityMap.ContainsKey(input) ? traceabilityMap[input] : null;
		}

		internal static ISet<CheckResultRootProperty2StructureElement> Check( LL.MDE.DataModels.EnAr.Package abstractionLevelP,LL.MDE.DataModels.XML.Tag parentSeDecomposition,LL.MDE.DataModels.XML.Tag stucture )
		{
			ISet<CheckResultRootProperty2StructureElement> result = new HashSet<CheckResultRootProperty2StructureElement>();ISet<MatchDomainAbstractionLevelP> matchDomainAbstractionLevelPs = CheckDomainAbstractionLevelP(abstractionLevelP);ISet<MatchDomainParentSeDecomposition> matchDomainParentSeDecompositions = CheckDomainParentSeDecomposition(parentSeDecomposition);ISet<MatchDomainStucture> matchDomainStuctures = CheckDomainStucture(stucture);foreach (MatchDomainAbstractionLevelP matchDomainAbstractionLevelP in matchDomainAbstractionLevelPs ) {foreach (MatchDomainParentSeDecomposition matchDomainParentSeDecomposition in matchDomainParentSeDecompositions ) {foreach (MatchDomainStucture matchDomainStucture in matchDomainStuctures ) {LL.MDE.DataModels.EnAr.Package bpP = matchDomainAbstractionLevelP.bpP;
			LL.MDE.DataModels.EnAr.Element bpPE = matchDomainAbstractionLevelP.bpPE;
			LL.MDE.DataModels.EnAr.Element childEl = matchDomainAbstractionLevelP.childEl;
			string id = matchDomainAbstractionLevelP.id;
			string elName = matchDomainAbstractionLevelP.elName;
			long reid = matchDomainAbstractionLevelP.reid;
			LL.MDE.DataModels.EnAr.Element classifierEl = matchDomainAbstractionLevelP.classifierEl;
			string classifierName = matchDomainAbstractionLevelP.classifierName;
			CheckResultRootProperty2StructureElement checkonlysMatch = new CheckResultRootProperty2StructureElement () {matchDomainAbstractionLevelP = matchDomainAbstractionLevelP,matchDomainParentSeDecomposition = matchDomainParentSeDecomposition,matchDomainStucture = matchDomainStucture,};
			result.Add(checkonlysMatch);			} // End foreach
						} // End foreach
						} // End foreach
				return result;
		}

		internal static ISet<MatchDomainAbstractionLevelP> CheckDomainAbstractionLevelP(LL.MDE.DataModels.EnAr.Package abstractionLevelP)
		{
			ISet<MatchDomainAbstractionLevelP> result = new HashSet<MatchDomainAbstractionLevelP>();
			foreach (LL.MDE.DataModels.EnAr.Package bpP  in abstractionLevelP.Packages.OfType<LL.MDE.DataModels.EnAr.Package>()) {
			LL.MDE.DataModels.EnAr.Element bpPE = bpP.Element;
			if (bpP.ParentPackage() == abstractionLevelP) {
			foreach (LL.MDE.DataModels.EnAr.Element childEl  in bpP.Elements.OfType<LL.MDE.DataModels.EnAr.Element>()) {
			if (bpPE.Stereotype == "block properties" && bpPE.ElementPackage() == bpP) {
			string id = childEl.ElementGUID;
			string elName = childEl.Name;
			long reid = childEl.ElementID;
			LL.MDE.DataModels.EnAr.Element classifierEl = childEl.Classifier();
			if (childEl.Type == "Object" && childEl.ElementPackage() == bpP) {
			string classifierName = classifierEl.Name;
			MatchDomainAbstractionLevelP match = new MatchDomainAbstractionLevelP() {
			abstractionLevelP = abstractionLevelP,
			bpP = bpP,
			bpPE = bpPE,
			childEl = childEl,
			id = id,
			elName = elName,
			reid = reid,
			classifierEl = classifierEl,
			classifierName = classifierName,
			};
			result.Add(match);
			}}}
			}}

			return result;
		}

		internal static ISet<MatchDomainParentSeDecomposition> CheckDomainParentSeDecomposition(LL.MDE.DataModels.XML.Tag parentSeDecomposition)
		{
			ISet<MatchDomainParentSeDecomposition> result = new HashSet<MatchDomainParentSeDecomposition>();
			MatchDomainParentSeDecomposition match = new MatchDomainParentSeDecomposition() {
			parentSeDecomposition = parentSeDecomposition,
			};
			result.Add(match);

			return result;
		}

		internal static ISet<MatchDomainStucture> CheckDomainStucture(LL.MDE.DataModels.XML.Tag stucture)
		{
			ISet<MatchDomainStucture> result = new HashSet<MatchDomainStucture>();
			MatchDomainStucture match = new MatchDomainStucture() {
			stucture = stucture,
			};
			result.Add(match);

			return result;
		}

		internal void CheckAndEnforce(LL.MDE.DataModels.EnAr.Package abstractionLevelP,LL.MDE.DataModels.XML.Tag parentSeDecomposition,LL.MDE.DataModels.XML.Tag stucture,LL.MDE.DataModels.XML.Tag structureElements )
		{
			CheckOnlyDomains input = new CheckOnlyDomains(abstractionLevelP,parentSeDecomposition,stucture);
			EnforceDomains output = new EnforceDomains(structureElements);
			if (traceabilityMap.ContainsKey(input) && !traceabilityMap[input].Equals(output))
			{
				throw new Exception("This relation has already been used with different enforced parameters!");
			}
			if (!traceabilityMap.ContainsKey(input))
			{
				ISet<CheckResultRootProperty2StructureElement> result = Check (abstractionLevelP,parentSeDecomposition,stucture);
				Enforce(result, structureElements);
				traceabilityMap[input] = output;
			}
		}

		internal void Enforce(ISet<CheckResultRootProperty2StructureElement> result, LL.MDE.DataModels.XML.Tag structureElements )
		{
			foreach (CheckResultRootProperty2StructureElement match in result)
			{
			// Extracting variables binded in source domains
			LL.MDE.DataModels.EnAr.Package abstractionLevelP = match.matchDomainAbstractionLevelP.abstractionLevelP;
			LL.MDE.DataModels.EnAr.Package bpP = match.matchDomainAbstractionLevelP.bpP;
			LL.MDE.DataModels.EnAr.Element bpPE = match.matchDomainAbstractionLevelP.bpPE;
			LL.MDE.DataModels.EnAr.Element childEl = match.matchDomainAbstractionLevelP.childEl;
			string id = match.matchDomainAbstractionLevelP.id;
			string elName = match.matchDomainAbstractionLevelP.elName;
			long reid = match.matchDomainAbstractionLevelP.reid;
			LL.MDE.DataModels.EnAr.Element classifierEl = match.matchDomainAbstractionLevelP.classifierEl;
			string classifierName = match.matchDomainAbstractionLevelP.classifierName;
			LL.MDE.DataModels.XML.Tag parentSeDecomposition = match.matchDomainParentSeDecomposition.parentSeDecomposition;
			LL.MDE.DataModels.XML.Tag stucture = match.matchDomainStucture.stucture;

			// Assigning variables binded in the where clause
			string name=elName + ':' + classifierName;

			// Enforcing each enforced domain
			MatchDomainStructureElements targetMatchDomainStructureElements = EnforceStructureElements(id,name,  structureElements );

			// Retrieving variables binded in the enforced domains
			LL.MDE.DataModels.XML.Tag fmStructureelement = targetMatchDomainStructureElements.fmStructureelement;
			LL.MDE.DataModels.XML.Tag fmSeDecomposition = targetMatchDomainStructureElements.fmSeDecomposition;
			LL.MDE.DataModels.XML.Attribute structureId = targetMatchDomainStructureElements.structureId;
			LL.MDE.DataModels.XML.Tag longName1 = targetMatchDomainStructureElements.longName1;
			LL.MDE.DataModels.XML.Tag l41 = targetMatchDomainStructureElements.l41;
			LL.MDE.DataModels.XML.Attribute lAttr1 = targetMatchDomainStructureElements.lAttr1;

			// Calling other relations as defined in the where clause
			new RelationCreateDecompositionLink(editor,transformation).CheckAndEnforce(structureId,parentSeDecomposition);new RelationBlockProperty2StructureElement(editor,transformation).CheckAndEnforce(childEl,fmSeDecomposition,structureElements);}
		}

		internal MatchDomainStructureElements EnforceStructureElements(string id,string name, LL.MDE.DataModels.XML.Tag structureElements)
		{
			MatchDomainStructureElements match = new MatchDomainStructureElements();

			// Contructing structureElements
			LL.MDE.DataModels.XML.Tag fmStructureelement = null;
			fmStructureelement =  (LL.MDE.DataModels.XML.Tag) editor.CreateNewObjectInField(structureElements, "childTags");

			// Contructing fmStructureelement
			editor.AddOrSetInField(fmStructureelement, "tagname", "FM-STRUCTURE-ELEMENT" );
			LL.MDE.DataModels.XML.Tag fmSeDecomposition = null;
			fmSeDecomposition =  (LL.MDE.DataModels.XML.Tag) editor.CreateNewObjectInField(fmStructureelement, "childTags");

			LL.MDE.DataModels.XML.Attribute structureId = null;
			structureId =  (LL.MDE.DataModels.XML.Attribute) editor.CreateNewObjectInField(fmStructureelement, "attributes");

			LL.MDE.DataModels.XML.Tag longName1 = null;
			longName1 =  (LL.MDE.DataModels.XML.Tag) editor.CreateNewObjectInField(fmStructureelement, "childTags");

			// Contructing fmSeDecomposition
			editor.AddOrSetInField(fmSeDecomposition, "tagname", "FM-SE-DECOMPOSITION" );

			// Contructing structureId
			editor.AddOrSetInField(structureId, "name", "ID" );
			editor.AddOrSetInField(structureId, "value", id );

			// Contructing longName1
			editor.AddOrSetInField(longName1, "tagname", "LONG-NAME" );
			LL.MDE.DataModels.XML.Tag l41 = null;
			l41 =  (LL.MDE.DataModels.XML.Tag) editor.CreateNewObjectInField(longName1, "childTags");

			// Contructing l41
			editor.AddOrSetInField(l41, "tagname", "L-4" );
			editor.AddOrSetInField(l41, "value", name );
			LL.MDE.DataModels.XML.Attribute lAttr1 = null;
			lAttr1 =  (LL.MDE.DataModels.XML.Attribute) editor.CreateNewObjectInField(l41, "attributes");

			// Contructing lAttr1
			editor.AddOrSetInField(lAttr1, "name", "L" );
			editor.AddOrSetInField(lAttr1, "value", "r" );

				// Return newly binded variables
								match.structureElements  = structureElements;
								match.fmStructureelement  = fmStructureelement;
								match.fmSeDecomposition  = fmSeDecomposition;
								match.structureId  = structureId;
								match.longName1  = longName1;
								match.l41  = l41;
								match.lAttr1  = lAttr1;
				return match;
		}

		public class CheckOnlyDomains : Tuple<Package,Tag,Tag>
		{
			public CheckOnlyDomains(Package abstractionLevelP,Tag parentSeDecomposition,Tag stucture)
				: base(abstractionLevelP,parentSeDecomposition,stucture)
			{
			}

			public Package abstractionLevelP
			{
				get { return Item1; }
			}

			public Tag parentSeDecomposition
			{
				get { return Item2; }
			}

			public Tag stucture
			{
				get { return Item3; }
			}
		}

		public class EnforceDomains : Tuple<Tag>
		{
			public EnforceDomains(Tag structureElements)
				: base(structureElements)
			{
			}

			public Tag structureElements
			{
				get { return Item1; }
			}
		}

		internal class CheckResultRootProperty2StructureElement
		{
			public MatchDomainAbstractionLevelP matchDomainAbstractionLevelP;
			public MatchDomainParentSeDecomposition matchDomainParentSeDecomposition;
			public MatchDomainStucture matchDomainStucture;
		}

		internal class MatchDomainAbstractionLevelP
		{
			public LL.MDE.DataModels.EnAr.Package abstractionLevelP;
			public LL.MDE.DataModels.EnAr.Package bpP;
			public LL.MDE.DataModels.EnAr.Element bpPE;
			public LL.MDE.DataModels.EnAr.Element childEl;
			public LL.MDE.DataModels.EnAr.Element classifierEl;
			public string classifierName;
			public string elName;
			public string id;
			public long reid;
		}

		internal class MatchDomainParentSeDecomposition
		{
			public LL.MDE.DataModels.XML.Tag parentSeDecomposition;
		}

		internal class MatchDomainStructureElements
		{
			public LL.MDE.DataModels.XML.Tag fmSeDecomposition;
			public LL.MDE.DataModels.XML.Tag fmStructureelement;
			public LL.MDE.DataModels.XML.Tag l41;
			public LL.MDE.DataModels.XML.Attribute lAttr1;
			public LL.MDE.DataModels.XML.Tag longName1;
			public LL.MDE.DataModels.XML.Tag structureElements;
			public LL.MDE.DataModels.XML.Attribute structureId;
		}

		internal class MatchDomainStucture
		{
			public LL.MDE.DataModels.XML.Tag stucture;
		}
	}
}