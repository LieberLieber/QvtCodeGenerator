namespace LL.MDE.Components.Qvt.Transformation.EA2FMEA
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using LL.MDE.Components.Qvt.Common;
	using LL.MDE.DataModels.EnAr;
	using LL.MDE.DataModels.XML;

	public class RelationAddStructureRoot
	{
		private readonly IMetaModelInterface editor;
		private readonly TransformationEA2FMEA transformation;

		private Dictionary<CheckOnlyDomains, EnforceDomains> traceabilityMap = new Dictionary<CheckOnlyDomains, EnforceDomains>();

		public RelationAddStructureRoot(IMetaModelInterface editor , TransformationEA2FMEA transformation )
		{
			this.editor = editor;this.transformation = transformation;
		}

		public EnforceDomains FindPreviousResult(LL.MDE.DataModels.EnAr.Package abstractionLevelP,LL.MDE.DataModels.XML.Tag structure)
		{
			CheckOnlyDomains input = new CheckOnlyDomains(abstractionLevelP,structure);
			return traceabilityMap.ContainsKey(input) ? traceabilityMap[input] : null;
		}

		internal static ISet<CheckResultAddStructureRoot> Check( LL.MDE.DataModels.EnAr.Package abstractionLevelP,LL.MDE.DataModels.XML.Tag structure )
		{
			ISet<CheckResultAddStructureRoot> result = new HashSet<CheckResultAddStructureRoot>();ISet<MatchDomainAbstractionLevelP> matchDomainAbstractionLevelPs = CheckDomainAbstractionLevelP(abstractionLevelP);ISet<MatchDomainStructure> matchDomainStructures = CheckDomainStructure(structure);foreach (MatchDomainAbstractionLevelP matchDomainAbstractionLevelP in matchDomainAbstractionLevelPs ) {foreach (MatchDomainStructure matchDomainStructure in matchDomainStructures ) {string alpName = matchDomainAbstractionLevelP.alpName;
			LL.MDE.DataModels.EnAr.Package fctsysP = matchDomainAbstractionLevelP.fctsysP;
			string fctsysName = matchDomainAbstractionLevelP.fctsysName;
			CheckResultAddStructureRoot checkonlysMatch = new CheckResultAddStructureRoot () {matchDomainAbstractionLevelP = matchDomainAbstractionLevelP,matchDomainStructure = matchDomainStructure,};
			result.Add(checkonlysMatch);			} // End foreach
						} // End foreach
				return result;
		}

		internal static ISet<MatchDomainAbstractionLevelP> CheckDomainAbstractionLevelP(LL.MDE.DataModels.EnAr.Package abstractionLevelP)
		{
			ISet<MatchDomainAbstractionLevelP> result = new HashSet<MatchDomainAbstractionLevelP>();
			string alpName = abstractionLevelP.Name;
			LL.MDE.DataModels.EnAr.Package fctsysP = abstractionLevelP.ParentPackage();
			string fctsysName = fctsysP.Name;
			MatchDomainAbstractionLevelP match = new MatchDomainAbstractionLevelP() {
			abstractionLevelP = abstractionLevelP,
			alpName = alpName,
			fctsysP = fctsysP,
			fctsysName = fctsysName,
			};
			result.Add(match);

			return result;
		}

		internal static ISet<MatchDomainStructure> CheckDomainStructure(LL.MDE.DataModels.XML.Tag structure)
		{
			ISet<MatchDomainStructure> result = new HashSet<MatchDomainStructure>();
			MatchDomainStructure match = new MatchDomainStructure() {
			structure = structure,
			};
			result.Add(match);

			return result;
		}

		internal void CheckAndEnforce(LL.MDE.DataModels.EnAr.Package abstractionLevelP,LL.MDE.DataModels.XML.Tag structure,LL.MDE.DataModels.XML.Tag structureElements )
		{
			CheckOnlyDomains input = new CheckOnlyDomains(abstractionLevelP,structure);
			EnforceDomains output = new EnforceDomains(structureElements);
			if (traceabilityMap.ContainsKey(input) && !traceabilityMap[input].Equals(output))
			{
				throw new Exception("This relation has already been used with different enforced parameters!");
			}
			if (!traceabilityMap.ContainsKey(input))
			{
				ISet<CheckResultAddStructureRoot> result = Check (abstractionLevelP,structure);
				Enforce(result, structureElements);
				traceabilityMap[input] = output;
			}
		}

		internal void Enforce(ISet<CheckResultAddStructureRoot> result, LL.MDE.DataModels.XML.Tag structureElements )
		{
			foreach (CheckResultAddStructureRoot match in result)
			{
			// Extracting variables binded in source domains
			LL.MDE.DataModels.EnAr.Package abstractionLevelP = match.matchDomainAbstractionLevelP.abstractionLevelP;
			string alpName = match.matchDomainAbstractionLevelP.alpName;
			LL.MDE.DataModels.EnAr.Package fctsysP = match.matchDomainAbstractionLevelP.fctsysP;
			string fctsysName = match.matchDomainAbstractionLevelP.fctsysName;
			LL.MDE.DataModels.XML.Tag structure = match.matchDomainStructure.structure;

			// Assigning variables binded in the where clause
			string elementName=fctsysName + ' ' + alpName;

			// Enforcing each enforced domain
			MatchDomainStructureElements targetMatchDomainStructureElements = EnforceStructureElements(elementName,  structureElements );

			// Retrieving variables binded in the enforced domains
			LL.MDE.DataModels.XML.Tag fmStructureelement = targetMatchDomainStructureElements.fmStructureelement;
			LL.MDE.DataModels.XML.Tag longName1 = targetMatchDomainStructureElements.longName1;
			LL.MDE.DataModels.XML.Tag l41 = targetMatchDomainStructureElements.l41;
			LL.MDE.DataModels.XML.Attribute lAttr1 = targetMatchDomainStructureElements.lAttr1;
			LL.MDE.DataModels.XML.Tag fmSeDecomposition = targetMatchDomainStructureElements.fmSeDecomposition;
			LL.MDE.DataModels.XML.Attribute structureId = targetMatchDomainStructureElements.structureId;

			// Calling other relations as defined in the where clause
			new RelationCreateStructureRootLink(editor,transformation).CheckAndEnforce(structureId,structure);new RelationRootProperty2StructureElement(editor,transformation).CheckAndEnforce(abstractionLevelP,fmSeDecomposition,structure,structureElements);}
		}

		internal MatchDomainStructureElements EnforceStructureElements(string elementName, LL.MDE.DataModels.XML.Tag structureElements)
		{
			MatchDomainStructureElements match = new MatchDomainStructureElements();

			// Contructing structureElements
			LL.MDE.DataModels.XML.Tag fmStructureelement = null;
			fmStructureelement =  (LL.MDE.DataModels.XML.Tag) editor.CreateNewObjectInField(structureElements, "childTags");

			// Contructing fmStructureelement
			editor.AddOrSetInField(fmStructureelement, "tagname", "FM-STRUCTURE-ELEMENT" );
			LL.MDE.DataModels.XML.Tag longName1 = null;
			longName1 =  (LL.MDE.DataModels.XML.Tag) editor.CreateNewObjectInField(fmStructureelement, "childTags");

			LL.MDE.DataModels.XML.Tag fmSeDecomposition = null;
			fmSeDecomposition =  (LL.MDE.DataModels.XML.Tag) editor.CreateNewObjectInField(fmStructureelement, "childTags");

			LL.MDE.DataModels.XML.Attribute structureId = null;
			structureId =  (LL.MDE.DataModels.XML.Attribute) editor.CreateNewObjectInField(fmStructureelement, "attributes");

			// Contructing longName1
			editor.AddOrSetInField(longName1, "tagname", "LONG-NAME" );
			LL.MDE.DataModels.XML.Tag l41 = null;
			l41 =  (LL.MDE.DataModels.XML.Tag) editor.CreateNewObjectInField(longName1, "childTags");

			// Contructing l41
			editor.AddOrSetInField(l41, "tagname", "L-4" );
			editor.AddOrSetInField(l41, "value", elementName );
			LL.MDE.DataModels.XML.Attribute lAttr1 = null;
			lAttr1 =  (LL.MDE.DataModels.XML.Attribute) editor.CreateNewObjectInField(l41, "attributes");

			// Contructing lAttr1
			editor.AddOrSetInField(lAttr1, "name", "L" );

			// Contructing fmSeDecomposition
			editor.AddOrSetInField(fmSeDecomposition, "tagname", "FM-SE-DECOMPOSITION" );

			// Contructing structureId
			editor.AddOrSetInField(structureId, "name", "ID" );
			editor.AddOrSetInField(structureId, "value", "root" );

				// Return newly binded variables
								match.structureElements  = structureElements;
								match.fmStructureelement  = fmStructureelement;
								match.longName1  = longName1;
								match.l41  = l41;
								match.lAttr1  = lAttr1;
								match.fmSeDecomposition  = fmSeDecomposition;
								match.structureId  = structureId;
				return match;
		}

		public class CheckOnlyDomains : Tuple<Package,Tag>
		{
			public CheckOnlyDomains(Package abstractionLevelP,Tag structure)
				: base(abstractionLevelP,structure)
			{
			}

			public Package abstractionLevelP
			{
				get { return Item1; }
			}

			public Tag structure
			{
				get { return Item2; }
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

		internal class CheckResultAddStructureRoot
		{
			public MatchDomainAbstractionLevelP matchDomainAbstractionLevelP;
			public MatchDomainStructure matchDomainStructure;
		}

		internal class MatchDomainAbstractionLevelP
		{
			public LL.MDE.DataModels.EnAr.Package abstractionLevelP;
			public string alpName;
			public string fctsysName;
			public LL.MDE.DataModels.EnAr.Package fctsysP;
		}

		internal class MatchDomainStructure
		{
			public LL.MDE.DataModels.XML.Tag structure;
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
	}
}