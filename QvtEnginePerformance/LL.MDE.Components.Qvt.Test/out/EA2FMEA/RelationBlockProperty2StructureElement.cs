namespace LL.MDE.Components.Qvt.Transformation.EA2FMEA
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using LL.MDE.Components.Qvt.Common;
	using LL.MDE.DataModels.EnAr;
	using LL.MDE.DataModels.XML;

	public class RelationBlockProperty2StructureElement
	{
		private readonly IMetaModelInterface editor;
		private readonly TransformationEA2FMEA transformation;

		private Dictionary<CheckOnlyDomains, EnforceDomains> traceabilityMap = new Dictionary<CheckOnlyDomains, EnforceDomains>();

		public RelationBlockProperty2StructureElement(IMetaModelInterface editor , TransformationEA2FMEA transformation )
		{
			this.editor = editor;this.transformation = transformation;
		}

		public EnforceDomains FindPreviousResult(LL.MDE.DataModels.EnAr.Element parentEl,LL.MDE.DataModels.XML.Tag parentSeDecomposition)
		{
			CheckOnlyDomains input = new CheckOnlyDomains(parentEl,parentSeDecomposition);
			return traceabilityMap.ContainsKey(input) ? traceabilityMap[input] : null;
		}

		internal static ISet<CheckResultBlockProperty2StructureElement> Check( LL.MDE.DataModels.EnAr.Element parentEl,LL.MDE.DataModels.XML.Tag parentSeDecomposition )
		{
			ISet<CheckResultBlockProperty2StructureElement> result = new HashSet<CheckResultBlockProperty2StructureElement>();ISet<MatchDomainParentEl> matchDomainParentEls = CheckDomainParentEl(parentEl);ISet<MatchDomainParentSeDecomposition> matchDomainParentSeDecompositions = CheckDomainParentSeDecomposition(parentSeDecomposition);foreach (MatchDomainParentEl matchDomainParentEl in matchDomainParentEls ) {foreach (MatchDomainParentSeDecomposition matchDomainParentSeDecomposition in matchDomainParentSeDecompositions ) {long sid = matchDomainParentEl.sid;
			LL.MDE.DataModels.EnAr.Connector aggregation = matchDomainParentEl.aggregation;
			int cid = matchDomainParentEl.cid;
			LL.MDE.DataModels.EnAr.Element childEl = matchDomainParentEl.childEl;
			string id = matchDomainParentEl.id;
			string elName = matchDomainParentEl.elName;
			LL.MDE.DataModels.EnAr.Element classifierEl = matchDomainParentEl.classifierEl;
			string classifierName = matchDomainParentEl.classifierName;
			CheckResultBlockProperty2StructureElement checkonlysMatch = new CheckResultBlockProperty2StructureElement () {matchDomainParentEl = matchDomainParentEl,matchDomainParentSeDecomposition = matchDomainParentSeDecomposition,};
			result.Add(checkonlysMatch);			} // End foreach
						} // End foreach
				return result;
		}

		internal static ISet<MatchDomainParentEl> CheckDomainParentEl(LL.MDE.DataModels.EnAr.Element parentEl)
		{
			ISet<MatchDomainParentEl> result = new HashSet<MatchDomainParentEl>();
			long sid = parentEl.ElementID;
			foreach (LL.MDE.DataModels.EnAr.Connector aggregation  in parentEl.Connectors.OfType<LL.MDE.DataModels.EnAr.Connector>()) {
			int cid = aggregation.ClientID;
			LL.MDE.DataModels.EnAr.Element childEl = aggregation.SourceElement();
			if (aggregation.SupplierID == sid && aggregation.Type == "Aggregation") {
			string id = childEl.ElementGUID;
			string elName = childEl.Name;
			LL.MDE.DataModels.EnAr.Element classifierEl = childEl.Classifier();
			if (childEl.Type == "Object") {
			string classifierName = classifierEl.Name;
			MatchDomainParentEl match = new MatchDomainParentEl() {
			parentEl = parentEl,
			sid = sid,
			aggregation = aggregation,
			cid = cid,
			childEl = childEl,
			id = id,
			elName = elName,
			classifierEl = classifierEl,
			classifierName = classifierName,
			};
			result.Add(match);
			}}}

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

		internal void CheckAndEnforce(LL.MDE.DataModels.EnAr.Element parentEl,LL.MDE.DataModels.XML.Tag parentSeDecomposition,LL.MDE.DataModels.XML.Tag structureElements )
		{
			CheckOnlyDomains input = new CheckOnlyDomains(parentEl,parentSeDecomposition);
			EnforceDomains output = new EnforceDomains(structureElements);
			if (traceabilityMap.ContainsKey(input) && !traceabilityMap[input].Equals(output))
			{
				throw new Exception("This relation has already been used with different enforced parameters!");
			}
			if (!traceabilityMap.ContainsKey(input))
			{
				ISet<CheckResultBlockProperty2StructureElement> result = Check (parentEl,parentSeDecomposition);
				Enforce(result, structureElements);
				traceabilityMap[input] = output;
			}
		}

		internal void Enforce(ISet<CheckResultBlockProperty2StructureElement> result, LL.MDE.DataModels.XML.Tag structureElements )
		{
			foreach (CheckResultBlockProperty2StructureElement match in result)
			{
			// Extracting variables binded in source domains
			LL.MDE.DataModels.EnAr.Element parentEl = match.matchDomainParentEl.parentEl;
			long sid = match.matchDomainParentEl.sid;
			LL.MDE.DataModels.EnAr.Connector aggregation = match.matchDomainParentEl.aggregation;
			int cid = match.matchDomainParentEl.cid;
			LL.MDE.DataModels.EnAr.Element childEl = match.matchDomainParentEl.childEl;
			string id = match.matchDomainParentEl.id;
			string elName = match.matchDomainParentEl.elName;
			LL.MDE.DataModels.EnAr.Element classifierEl = match.matchDomainParentEl.classifierEl;
			string classifierName = match.matchDomainParentEl.classifierName;
			LL.MDE.DataModels.XML.Tag parentSeDecomposition = match.matchDomainParentSeDecomposition.parentSeDecomposition;

			// Assigning variables binded in the where clause
			string name=elName + ':' + classifierName;

			// Enforcing each enforced domain
			MatchDomainStructureElements targetMatchDomainStructureElements = EnforceStructureElements(name,id,  structureElements );

			// Retrieving variables binded in the enforced domains
			LL.MDE.DataModels.XML.Tag fmStructureelement = targetMatchDomainStructureElements.fmStructureelement;
			LL.MDE.DataModels.XML.Tag fmSeDecomposition = targetMatchDomainStructureElements.fmSeDecomposition;
			LL.MDE.DataModels.XML.Tag longName1 = targetMatchDomainStructureElements.longName1;
			LL.MDE.DataModels.XML.Tag l41 = targetMatchDomainStructureElements.l41;
			LL.MDE.DataModels.XML.Attribute lAttr1 = targetMatchDomainStructureElements.lAttr1;
			LL.MDE.DataModels.XML.Attribute structureId = targetMatchDomainStructureElements.structureId;

			// Calling other relations as defined in the where clause
			new RelationCreateDecompositionLink(editor,transformation).CheckAndEnforce(structureId,parentSeDecomposition);new RelationBlockProperty2StructureElement(editor,transformation).CheckAndEnforce(childEl,fmSeDecomposition,structureElements);}
		}

		internal MatchDomainStructureElements EnforceStructureElements(string name,string id, LL.MDE.DataModels.XML.Tag structureElements)
		{
			MatchDomainStructureElements match = new MatchDomainStructureElements();

			// Contructing structureElements
			LL.MDE.DataModels.XML.Tag fmStructureelement = null;
			fmStructureelement =  (LL.MDE.DataModels.XML.Tag) editor.CreateNewObjectInField(structureElements, "childTags");

			// Contructing fmStructureelement
			editor.AddOrSetInField(fmStructureelement, "tagname", "FM-STRUCTURE-ELEMENT" );
			LL.MDE.DataModels.XML.Tag fmSeDecomposition = null;
			fmSeDecomposition =  (LL.MDE.DataModels.XML.Tag) editor.CreateNewObjectInField(fmStructureelement, "childTags");

			LL.MDE.DataModels.XML.Tag longName1 = null;
			longName1 =  (LL.MDE.DataModels.XML.Tag) editor.CreateNewObjectInField(fmStructureelement, "childTags");

			LL.MDE.DataModels.XML.Attribute structureId = null;
			structureId =  (LL.MDE.DataModels.XML.Attribute) editor.CreateNewObjectInField(fmStructureelement, "attributes");

			// Contructing fmSeDecomposition
			editor.AddOrSetInField(fmSeDecomposition, "tagname", "FM-SE-DECOMPOSITION" );

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
			editor.AddOrSetInField(lAttr1, "value", "bp2se" );

			// Contructing structureId
			editor.AddOrSetInField(structureId, "name", "ID" );
			editor.AddOrSetInField(structureId, "value", id );

				// Return newly binded variables
								match.structureElements  = structureElements;
								match.fmStructureelement  = fmStructureelement;
								match.fmSeDecomposition  = fmSeDecomposition;
								match.longName1  = longName1;
								match.l41  = l41;
								match.lAttr1  = lAttr1;
								match.structureId  = structureId;
				return match;
		}

		public class CheckOnlyDomains : Tuple<Element,Tag>
		{
			public CheckOnlyDomains(Element parentEl,Tag parentSeDecomposition)
				: base(parentEl,parentSeDecomposition)
			{
			}

			public Element parentEl
			{
				get { return Item1; }
			}

			public Tag parentSeDecomposition
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

		internal class CheckResultBlockProperty2StructureElement
		{
			public MatchDomainParentEl matchDomainParentEl;
			public MatchDomainParentSeDecomposition matchDomainParentSeDecomposition;
		}

		internal class MatchDomainParentEl
		{
			public LL.MDE.DataModels.EnAr.Connector aggregation;
			public LL.MDE.DataModels.EnAr.Element childEl;
			public int cid;
			public LL.MDE.DataModels.EnAr.Element classifierEl;
			public string classifierName;
			public string elName;
			public string id;
			public LL.MDE.DataModels.EnAr.Element parentEl;
			public long sid;
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
	}
}