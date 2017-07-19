namespace LL.MDE.Components.Qvt.Transformation.EA2FMEA
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using LL.MDE.Components.Qvt.Common;
	using LL.MDE.DataModels.EnAr;
	using LL.MDE.DataModels.XML;

	public class RelationCreateStructureRootLink
	{
		private readonly IMetaModelInterface editor;
		private readonly TransformationEA2FMEA transformation;

		private Dictionary<CheckOnlyDomains, EnforceDomains> traceabilityMap = new Dictionary<CheckOnlyDomains, EnforceDomains>();

		public RelationCreateStructureRootLink(IMetaModelInterface editor , TransformationEA2FMEA transformation )
		{
			this.editor = editor;this.transformation = transformation;
		}

		public EnforceDomains FindPreviousResult(LL.MDE.DataModels.XML.Attribute structureElementRef)
		{
			CheckOnlyDomains input = new CheckOnlyDomains(structureElementRef);
			return traceabilityMap.ContainsKey(input) ? traceabilityMap[input] : null;
		}

		internal static ISet<CheckResultCreateStructureRootLink> Check( LL.MDE.DataModels.XML.Attribute structureElementRef )
		{
			ISet<CheckResultCreateStructureRootLink> result = new HashSet<CheckResultCreateStructureRootLink>();ISet<MatchDomainStructureElementRef> matchDomainStructureElementRefs = CheckDomainStructureElementRef(structureElementRef);foreach (MatchDomainStructureElementRef matchDomainStructureElementRef in matchDomainStructureElementRefs ) {string id = matchDomainStructureElementRef.id;
			CheckResultCreateStructureRootLink checkonlysMatch = new CheckResultCreateStructureRootLink () {matchDomainStructureElementRef = matchDomainStructureElementRef,};
			result.Add(checkonlysMatch);			} // End foreach
				return result;
		}

		internal static ISet<MatchDomainStructureElementRef> CheckDomainStructureElementRef(LL.MDE.DataModels.XML.Attribute structureElementRef)
		{
			ISet<MatchDomainStructureElementRef> result = new HashSet<MatchDomainStructureElementRef>();
			string id = structureElementRef.value;
			MatchDomainStructureElementRef match = new MatchDomainStructureElementRef() {
			structureElementRef = structureElementRef,
			id = id,
			};
			result.Add(match);

			return result;
		}

		internal void CheckAndEnforce(LL.MDE.DataModels.XML.Attribute structureElementRef,LL.MDE.DataModels.XML.Tag fmStructure )
		{
			CheckOnlyDomains input = new CheckOnlyDomains(structureElementRef);
			EnforceDomains output = new EnforceDomains(fmStructure);
			if (traceabilityMap.ContainsKey(input) && !traceabilityMap[input].Equals(output))
			{
				throw new Exception("This relation has already been used with different enforced parameters!");
			}
			if (!traceabilityMap.ContainsKey(input))
			{
				ISet<CheckResultCreateStructureRootLink> result = Check (structureElementRef);
				Enforce(result, fmStructure);
				traceabilityMap[input] = output;
			}
		}

		internal void Enforce(ISet<CheckResultCreateStructureRootLink> result, LL.MDE.DataModels.XML.Tag fmStructure )
		{
			foreach (CheckResultCreateStructureRootLink match in result)
			{
			// Extracting variables binded in source domains
			LL.MDE.DataModels.XML.Attribute structureElementRef = match.matchDomainStructureElementRef.structureElementRef;
			string id = match.matchDomainStructureElementRef.id;

			// Enforcing each enforced domain
			MatchDomainFmStructure targetMatchDomainFmStructure = EnforceFmStructure(id,  fmStructure );

			// Retrieving variables binded in the enforced domains
			LL.MDE.DataModels.XML.Tag fmStructureRoot = targetMatchDomainFmStructure.fmStructureRoot;
			LL.MDE.DataModels.XML.Tag fmStructureElementRef = targetMatchDomainFmStructure.fmStructureElementRef;
			LL.MDE.DataModels.XML.Attribute idRef = targetMatchDomainFmStructure.idRef;
			}
		}

		internal MatchDomainFmStructure EnforceFmStructure(string id, LL.MDE.DataModels.XML.Tag fmStructure)
		{
			MatchDomainFmStructure match = new MatchDomainFmStructure();

			// Contructing fmStructure
			LL.MDE.DataModels.XML.Tag fmStructureRoot = null;
			fmStructureRoot =  (LL.MDE.DataModels.XML.Tag) editor.CreateNewObjectInField(fmStructure, "childTags");

			// Contructing fmStructureRoot
			editor.AddOrSetInField(fmStructureRoot, "tagname", "FM-STRUCTURE-ROOT" );
			LL.MDE.DataModels.XML.Tag fmStructureElementRef = null;
			fmStructureElementRef =  (LL.MDE.DataModels.XML.Tag) editor.CreateNewObjectInField(fmStructureRoot, "childTags");

			// Contructing fmStructureElementRef
			editor.AddOrSetInField(fmStructureElementRef, "tagname", "FM-STRUCTURE-ELEMENT-REF" );
			LL.MDE.DataModels.XML.Attribute idRef = null;
			idRef =  (LL.MDE.DataModels.XML.Attribute) editor.CreateNewObjectInField(fmStructureElementRef, "attributes");

			// Contructing idRef
			editor.AddOrSetInField(idRef, "name", "ID-REF" );
			editor.AddOrSetInField(idRef, "value", id );

				// Return newly binded variables
								match.fmStructure  = fmStructure;
								match.fmStructureRoot  = fmStructureRoot;
								match.fmStructureElementRef  = fmStructureElementRef;
								match.idRef  = idRef;
				return match;
		}

		public class CheckOnlyDomains : Tuple<Attribute>
		{
			public CheckOnlyDomains(Attribute structureElementRef)
				: base(structureElementRef)
			{
			}

			public Attribute structureElementRef
			{
				get { return Item1; }
			}
		}

		public class EnforceDomains : Tuple<Tag>
		{
			public EnforceDomains(Tag fmStructure)
				: base(fmStructure)
			{
			}

			public Tag fmStructure
			{
				get { return Item1; }
			}
		}

		internal class CheckResultCreateStructureRootLink
		{
			public MatchDomainStructureElementRef matchDomainStructureElementRef;
		}

		internal class MatchDomainFmStructure
		{
			public LL.MDE.DataModels.XML.Tag fmStructure;
			public LL.MDE.DataModels.XML.Tag fmStructureElementRef;
			public LL.MDE.DataModels.XML.Tag fmStructureRoot;
			public LL.MDE.DataModels.XML.Attribute idRef;
		}

		internal class MatchDomainStructureElementRef
		{
			public string id;
			public LL.MDE.DataModels.XML.Attribute structureElementRef;
		}
	}
}