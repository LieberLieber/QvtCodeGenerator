namespace LL.MDE.Components.Qvt.Transformation.EA2FMEA
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using LL.MDE.Components.Qvt.Common;
	using LL.MDE.DataModels.EnAr;
	using LL.MDE.DataModels.XML;

	public class RelationCreateProjectStructureLink
	{
		private readonly IMetaModelInterface editor;
		private readonly TransformationEA2FMEA transformation;

		private Dictionary<CheckOnlyDomains, EnforceDomains> traceabilityMap = new Dictionary<CheckOnlyDomains, EnforceDomains>();

		public RelationCreateProjectStructureLink(IMetaModelInterface editor , TransformationEA2FMEA transformation )
		{
			this.editor = editor;this.transformation = transformation;
		}

		public EnforceDomains FindPreviousResult(LL.MDE.DataModels.XML.Attribute structureIdRef)
		{
			CheckOnlyDomains input = new CheckOnlyDomains(structureIdRef);
			return traceabilityMap.ContainsKey(input) ? traceabilityMap[input] : null;
		}

		internal static ISet<CheckResultCreateProjectStructureLink> Check( LL.MDE.DataModels.XML.Attribute structureIdRef )
		{
			ISet<CheckResultCreateProjectStructureLink> result = new HashSet<CheckResultCreateProjectStructureLink>();ISet<MatchDomainStructureIdRef> matchDomainStructureIdRefs = CheckDomainStructureIdRef(structureIdRef);foreach (MatchDomainStructureIdRef matchDomainStructureIdRef in matchDomainStructureIdRefs ) {string id = matchDomainStructureIdRef.id;
			CheckResultCreateProjectStructureLink checkonlysMatch = new CheckResultCreateProjectStructureLink () {matchDomainStructureIdRef = matchDomainStructureIdRef,};
			result.Add(checkonlysMatch);			} // End foreach
				return result;
		}

		internal static ISet<MatchDomainStructureIdRef> CheckDomainStructureIdRef(LL.MDE.DataModels.XML.Attribute structureIdRef)
		{
			ISet<MatchDomainStructureIdRef> result = new HashSet<MatchDomainStructureIdRef>();
			string id = structureIdRef.value;
			MatchDomainStructureIdRef match = new MatchDomainStructureIdRef() {
			structureIdRef = structureIdRef,
			id = id,
			};
			result.Add(match);

			return result;
		}

		internal void CheckAndEnforce(LL.MDE.DataModels.XML.Attribute structureIdRef,LL.MDE.DataModels.XML.Tag fmStructureRefs )
		{
			CheckOnlyDomains input = new CheckOnlyDomains(structureIdRef);
			EnforceDomains output = new EnforceDomains(fmStructureRefs);
			if (traceabilityMap.ContainsKey(input) && !traceabilityMap[input].Equals(output))
			{
				throw new Exception("This relation has already been used with different enforced parameters!");
			}
			if (!traceabilityMap.ContainsKey(input))
			{
				ISet<CheckResultCreateProjectStructureLink> result = Check (structureIdRef);
				Enforce(result, fmStructureRefs);
				traceabilityMap[input] = output;
			}
		}

		internal void Enforce(ISet<CheckResultCreateProjectStructureLink> result, LL.MDE.DataModels.XML.Tag fmStructureRefs )
		{
			foreach (CheckResultCreateProjectStructureLink match in result)
			{
			// Extracting variables binded in source domains
			LL.MDE.DataModels.XML.Attribute structureIdRef = match.matchDomainStructureIdRef.structureIdRef;
			string id = match.matchDomainStructureIdRef.id;

			// Enforcing each enforced domain
			MatchDomainFmStructureRefs targetMatchDomainFmStructureRefs = EnforceFmStructureRefs(id,  fmStructureRefs );

			// Retrieving variables binded in the enforced domains
			LL.MDE.DataModels.XML.Tag fmStructureRef = targetMatchDomainFmStructureRefs.fmStructureRef;
			LL.MDE.DataModels.XML.Attribute idRef = targetMatchDomainFmStructureRefs.idRef;
			}
		}

		internal MatchDomainFmStructureRefs EnforceFmStructureRefs(string id, LL.MDE.DataModels.XML.Tag fmStructureRefs)
		{
			MatchDomainFmStructureRefs match = new MatchDomainFmStructureRefs();

			// Contructing fmStructureRefs
			LL.MDE.DataModels.XML.Tag fmStructureRef = null;
			fmStructureRef =  (LL.MDE.DataModels.XML.Tag) editor.CreateNewObjectInField(fmStructureRefs, "childTags");

			// Contructing fmStructureRef
			editor.AddOrSetInField(fmStructureRef, "tagname", "FM-STRUCTURE-REF" );
			LL.MDE.DataModels.XML.Attribute idRef = null;
			idRef =  (LL.MDE.DataModels.XML.Attribute) editor.CreateNewObjectInField(fmStructureRef, "attributes");

			// Contructing idRef
			editor.AddOrSetInField(idRef, "name", "ID-REF" );
			editor.AddOrSetInField(idRef, "value", id );

				// Return newly binded variables
								match.fmStructureRefs  = fmStructureRefs;
								match.fmStructureRef  = fmStructureRef;
								match.idRef  = idRef;
				return match;
		}

		public class CheckOnlyDomains : Tuple<Attribute>
		{
			public CheckOnlyDomains(Attribute structureIdRef)
				: base(structureIdRef)
			{
			}

			public Attribute structureIdRef
			{
				get { return Item1; }
			}
		}

		public class EnforceDomains : Tuple<Tag>
		{
			public EnforceDomains(Tag fmStructureRefs)
				: base(fmStructureRefs)
			{
			}

			public Tag fmStructureRefs
			{
				get { return Item1; }
			}
		}

		internal class CheckResultCreateProjectStructureLink
		{
			public MatchDomainStructureIdRef matchDomainStructureIdRef;
		}

		internal class MatchDomainFmStructureRefs
		{
			public LL.MDE.DataModels.XML.Tag fmStructureRef;
			public LL.MDE.DataModels.XML.Tag fmStructureRefs;
			public LL.MDE.DataModels.XML.Attribute idRef;
		}

		internal class MatchDomainStructureIdRef
		{
			public string id;
			public LL.MDE.DataModels.XML.Attribute structureIdRef;
		}
	}
}