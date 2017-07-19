namespace LL.MDE.Components.Qvt.Transformation.umlToRdbms
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using LL.MDE.Components.Qvt.Common;
	using LL.MDE.DataModels.SimpleRDBMS;
	using LL.MDE.DataModels.SimpleUML;

	public class RelationPackageToSchema
	{
		private readonly IMetaModelInterface editor;
		private readonly Dictionary<CheckOnlyDomains, EnforceDomains> traceabilityMap = new Dictionary<CheckOnlyDomains, EnforceDomains>();
		private readonly TransformationumlToRdbms transformation;

		public RelationPackageToSchema(IMetaModelInterface editor , TransformationumlToRdbms transformation )
		{
			this.editor = editor;this.transformation = transformation;
		}

		public void CheckAndEnforce(LL.MDE.DataModels.SimpleUML.Package p,LL.MDE.DataModels.SimpleRDBMS.Schema s )
		{
			CheckOnlyDomains input = new CheckOnlyDomains(p);
			EnforceDomains output = new EnforceDomains(s);
			if (traceabilityMap.ContainsKey(input) && !traceabilityMap[input].Equals(output))
			{
				throw new Exception("This relation has already been used with different enforced parameters!");
			}
			if (!traceabilityMap.ContainsKey(input))
			{
				ISet<CheckResultPackageToSchema> result = Check (p);
				Enforce(result, s);
				traceabilityMap[input] = output;
			}
		}

		public EnforceDomains FindPreviousResult(LL.MDE.DataModels.SimpleUML.Package p)
		{
			CheckOnlyDomains input = new CheckOnlyDomains(p);
			return traceabilityMap.ContainsKey(input) ? traceabilityMap[input] : null;
		}

		internal static ISet<CheckResultPackageToSchema> Check( LL.MDE.DataModels.SimpleUML.Package p )
		{
			ISet<CheckResultPackageToSchema> result = new HashSet<CheckResultPackageToSchema>();ISet<MatchDomainP> matchDomainPs = CheckDomainP(p);
			foreach (MatchDomainP matchDomainP in matchDomainPs ) {
			string pn = matchDomainP.pn;

			CheckResultPackageToSchema checkonlysMatch = new CheckResultPackageToSchema () {matchDomainP = matchDomainP,
			};
			result.Add(checkonlysMatch);			} // End foreach
				return result;
		}

		internal static ISet<MatchDomainP> CheckDomainP(LL.MDE.DataModels.SimpleUML.Package p)
		{
			ISet<MatchDomainP> result = new HashSet<MatchDomainP>();
			if (p != null) {
			string pn = (string)p.name;
			MatchDomainP match = new MatchDomainP() {
			p = p,
			pn = pn,
			};
			result.Add(match);
			}

			return result;
		}

		internal void Enforce(ISet<CheckResultPackageToSchema> result, LL.MDE.DataModels.SimpleRDBMS.Schema s )
		{
			foreach (CheckResultPackageToSchema match in result)
			{
			// Extracting variables binded in source domains
			LL.MDE.DataModels.SimpleUML.Package p = match.matchDomainP.p;
			string pn = match.matchDomainP.pn;

			// Enforcing each enforced domain
			MatchDomainS targetMatchDomainS = EnforceS(match, pn,s);

			// Retrieving variables binded in the enforced domains

			}
		}

		internal MatchDomainS EnforceS(CheckResultPackageToSchema checkresult, string pn,LL.MDE.DataModels.SimpleRDBMS.Schema s)
		{
			MatchDomainS match = new MatchDomainS();LL.MDE.DataModels.SimpleUML.Package p = checkresult.matchDomainP.p;

			// Contructing s
			editor.AddOrSetInField(s, "name", pn );

				// Return newly binded variables
								match.s  = s;
				return match;
		}

		public class CheckOnlyDomains : Tuple<Package>
		{
			public CheckOnlyDomains(Package p)
				: base(p)
			{
			}

			public Package p
			{
				get { return Item1; }
			}
		}

		public class EnforceDomains : Tuple<Schema>
		{
			public EnforceDomains(Schema s)
				: base(s)
			{
			}

			public Schema s
			{
				get { return Item1; }
			}
		}

		internal class CheckResultPackageToSchema
		{
			public MatchDomainP matchDomainP;
		}

		internal class MatchDomainP
		{
			public LL.MDE.DataModels.SimpleUML.Package p;
			public string pn;
		}

		internal class MatchDomainS
		{
			public LL.MDE.DataModels.SimpleRDBMS.Schema s;
		}
	}
}