namespace LL.MDE.Components.Qvt.Transformation.EA2FMEA
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using LL.MDE.Components.Qvt.Common;
	using LL.MDE.DataModels.EnAr;
	using LL.MDE.DataModels.XML;

	public class RelationCreateDecompositionLink
	{
		private readonly IMetaModelInterface editor;
		private readonly TransformationEA2FMEA transformation;

		private Dictionary<CheckOnlyDomains, EnforceDomains> traceabilityMap = new Dictionary<CheckOnlyDomains, EnforceDomains>();

		public RelationCreateDecompositionLink(IMetaModelInterface editor , TransformationEA2FMEA transformation )
		{
			this.editor = editor;this.transformation = transformation;
		}

		public EnforceDomains FindPreviousResult(LL.MDE.DataModels.XML.Attribute tagId)
		{
			CheckOnlyDomains input = new CheckOnlyDomains(tagId);
			return traceabilityMap.ContainsKey(input) ? traceabilityMap[input] : null;
		}

		internal static ISet<CheckResultCreateDecompositionLink> Check( LL.MDE.DataModels.XML.Attribute tagId )
		{
			ISet<CheckResultCreateDecompositionLink> result = new HashSet<CheckResultCreateDecompositionLink>();ISet<MatchDomainTagId> matchDomainTagIds = CheckDomainTagId(tagId);foreach (MatchDomainTagId matchDomainTagId in matchDomainTagIds ) {string id = matchDomainTagId.id;
			CheckResultCreateDecompositionLink checkonlysMatch = new CheckResultCreateDecompositionLink () {matchDomainTagId = matchDomainTagId,};
			result.Add(checkonlysMatch);			} // End foreach
				return result;
		}

		internal static ISet<MatchDomainTagId> CheckDomainTagId(LL.MDE.DataModels.XML.Attribute tagId)
		{
			ISet<MatchDomainTagId> result = new HashSet<MatchDomainTagId>();
			string id = tagId.value;
			MatchDomainTagId match = new MatchDomainTagId() {
			tagId = tagId,
			id = id,
			};
			result.Add(match);

			return result;
		}

		internal void CheckAndEnforce(LL.MDE.DataModels.XML.Attribute tagId,LL.MDE.DataModels.XML.Tag decompositions )
		{
			CheckOnlyDomains input = new CheckOnlyDomains(tagId);
			EnforceDomains output = new EnforceDomains(decompositions);
			if (traceabilityMap.ContainsKey(input) && !traceabilityMap[input].Equals(output))
			{
				throw new Exception("This relation has already been used with different enforced parameters!");
			}
			if (!traceabilityMap.ContainsKey(input))
			{
				ISet<CheckResultCreateDecompositionLink> result = Check (tagId);
				Enforce(result, decompositions);
				traceabilityMap[input] = output;
			}
		}

		internal void Enforce(ISet<CheckResultCreateDecompositionLink> result, LL.MDE.DataModels.XML.Tag decompositions )
		{
			foreach (CheckResultCreateDecompositionLink match in result)
			{
			// Extracting variables binded in source domains
			LL.MDE.DataModels.XML.Attribute tagId = match.matchDomainTagId.tagId;
			string id = match.matchDomainTagId.id;

			// Enforcing each enforced domain
			MatchDomainDecompositions targetMatchDomainDecompositions = EnforceDecompositions(id,  decompositions );

			// Retrieving variables binded in the enforced domains
			LL.MDE.DataModels.XML.Tag decomposition = targetMatchDomainDecompositions.decomposition;
			LL.MDE.DataModels.XML.Attribute idRef = targetMatchDomainDecompositions.idRef;
			LL.MDE.DataModels.XML.Attribute fidClassAttr = targetMatchDomainDecompositions.fidClassAttr;
			}
		}

		internal MatchDomainDecompositions EnforceDecompositions(string id, LL.MDE.DataModels.XML.Tag decompositions)
		{
			MatchDomainDecompositions match = new MatchDomainDecompositions();

			// Contructing decompositions
			LL.MDE.DataModels.XML.Tag decomposition = null;
			decomposition =  (LL.MDE.DataModels.XML.Tag) editor.CreateNewObjectInField(decompositions, "childTags");

			// Contructing decomposition
			editor.AddOrSetInField(decomposition, "tagname", "FM-STRUCTURE-ELEMENT-REF" );
			LL.MDE.DataModels.XML.Attribute idRef = null;
			idRef =  (LL.MDE.DataModels.XML.Attribute) editor.CreateNewObjectInField(decomposition, "attributes");

			LL.MDE.DataModels.XML.Attribute fidClassAttr = null;
			fidClassAttr =  (LL.MDE.DataModels.XML.Attribute) editor.CreateNewObjectInField(decomposition, "attributes");

			// Contructing idRef
			editor.AddOrSetInField(idRef, "name", "ID-REF" );
			editor.AddOrSetInField(idRef, "value", id );

			// Contructing fidClassAttr
			editor.AddOrSetInField(fidClassAttr, "name", "F-ID-CLASS" );
			editor.AddOrSetInField(fidClassAttr, "value", "FM-STRUCTURE-ELEMENT" );

				// Return newly binded variables
								match.decompositions  = decompositions;
								match.decomposition  = decomposition;
								match.idRef  = idRef;
								match.fidClassAttr  = fidClassAttr;
				return match;
		}

		public class CheckOnlyDomains : Tuple<Attribute>
		{
			public CheckOnlyDomains(Attribute tagId)
				: base(tagId)
			{
			}

			public Attribute tagId
			{
				get { return Item1; }
			}
		}

		public class EnforceDomains : Tuple<Tag>
		{
			public EnforceDomains(Tag decompositions)
				: base(decompositions)
			{
			}

			public Tag decompositions
			{
				get { return Item1; }
			}
		}

		internal class CheckResultCreateDecompositionLink
		{
			public MatchDomainTagId matchDomainTagId;
		}

		internal class MatchDomainDecompositions
		{
			public LL.MDE.DataModels.XML.Tag decomposition;
			public LL.MDE.DataModels.XML.Tag decompositions;
			public LL.MDE.DataModels.XML.Attribute fidClassAttr;
			public LL.MDE.DataModels.XML.Attribute idRef;
		}

		internal class MatchDomainTagId
		{
			public string id;
			public LL.MDE.DataModels.XML.Attribute tagId;
		}
	}
}