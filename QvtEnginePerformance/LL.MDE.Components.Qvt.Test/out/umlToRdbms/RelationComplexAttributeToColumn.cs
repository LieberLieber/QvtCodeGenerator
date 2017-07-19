namespace LL.MDE.Components.Qvt.Transformation.umlToRdbms
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using LL.MDE.Components.Qvt.Common;
	using LL.MDE.DataModels.SimpleRDBMS;
	using LL.MDE.DataModels.SimpleUML;

	public class RelationComplexAttributeToColumn
	{
		private readonly IMetaModelInterface editor;
		private readonly Dictionary<CheckOnlyDomains, EnforceDomains> traceabilityMap = new Dictionary<CheckOnlyDomains, EnforceDomains>();
		private readonly TransformationumlToRdbms transformation;

		public RelationComplexAttributeToColumn(IMetaModelInterface editor , TransformationumlToRdbms transformation )
		{
			this.editor = editor;this.transformation = transformation;
		}

		public EnforceDomains FindPreviousResult(LL.MDE.DataModels.SimpleUML.Class c,string prefix)
		{
			CheckOnlyDomains input = new CheckOnlyDomains(c,prefix);
			return traceabilityMap.ContainsKey(input) ? traceabilityMap[input] : null;
		}

		internal static ISet<CheckResultComplexAttributeToColumn> Check( LL.MDE.DataModels.SimpleUML.Class c,string prefix )
		{
			ISet<CheckResultComplexAttributeToColumn> result = new HashSet<CheckResultComplexAttributeToColumn>();ISet<MatchDomainC> matchDomainCs = CheckDomainC(c);
			foreach (MatchDomainC matchDomainC in matchDomainCs ) {
			LL.MDE.DataModels.SimpleUML.Attribute a = matchDomainC.a;
			string an = matchDomainC.an;
			LL.MDE.DataModels.SimpleUML.Class tc = matchDomainC.tc;

			CheckResultComplexAttributeToColumn checkonlysMatch = new CheckResultComplexAttributeToColumn () {matchDomainC = matchDomainC,
			};
			result.Add(checkonlysMatch);			} // End foreach
				return result;
		}

		internal static ISet<MatchDomainC> CheckDomainC(LL.MDE.DataModels.SimpleUML.Class c)
		{
			ISet<MatchDomainC> result = new HashSet<MatchDomainC>();
			if (c != null) {
			foreach (LL.MDE.DataModels.SimpleUML.Attribute a  in c.attribute.OfType<LL.MDE.DataModels.SimpleUML.Attribute>()) {
			if (a != null) {
			string an = (string)a.name;
			LL.MDE.DataModels.SimpleUML.Class tc = (LL.MDE.DataModels.SimpleUML.Class)a.type;
			if (tc != null) {
			MatchDomainC match = new MatchDomainC() {
			c = c,
			a = a,
			an = an,
			tc = tc,
			};
			result.Add(match);
			}
			}
			}
			}

			return result;
		}

		internal void CheckAndEnforce(LL.MDE.DataModels.SimpleUML.Class c,LL.MDE.DataModels.SimpleRDBMS.Table t,string prefix )
		{
			CheckOnlyDomains input = new CheckOnlyDomains(c,prefix);
			EnforceDomains output = new EnforceDomains(t);
			if (traceabilityMap.ContainsKey(input) && !traceabilityMap[input].Equals(output))
			{
				throw new Exception("This relation has already been used with different enforced parameters!");
			}
			if (!traceabilityMap.ContainsKey(input))
			{
				ISet<CheckResultComplexAttributeToColumn> result = Check (c,prefix);
				Enforce(result, t,prefix);
				traceabilityMap[input] = output;
			}
		}

		internal void Enforce(ISet<CheckResultComplexAttributeToColumn> result, LL.MDE.DataModels.SimpleRDBMS.Table t,string prefix )
		{
			foreach (CheckResultComplexAttributeToColumn match in result)
			{
			// Extracting variables binded in source domains
			LL.MDE.DataModels.SimpleUML.Class c = match.matchDomainC.c;
			LL.MDE.DataModels.SimpleUML.Attribute a = match.matchDomainC.a;
			string an = match.matchDomainC.an;
			LL.MDE.DataModels.SimpleUML.Class tc = match.matchDomainC.tc;

			// Assigning variables binded in the where clause
			string newprefix = prefix + "_" + an;

			// Enforcing each enforced domain
			MatchDomainT targetMatchDomainT = EnforceT(match, t);

			// Retrieving variables binded in the enforced domains

			// Calling other relations as defined in the where clause
			transformation.RelationAttributeToColumn.CheckAndEnforce(tc,t,newprefix);
				}
		}

		internal MatchDomainT EnforceT(CheckResultComplexAttributeToColumn checkresult, LL.MDE.DataModels.SimpleRDBMS.Table t)
		{
			MatchDomainT match = new MatchDomainT();LL.MDE.DataModels.SimpleUML.Class c = checkresult.matchDomainC.c;
			LL.MDE.DataModels.SimpleUML.Attribute a = checkresult.matchDomainC.a;
			LL.MDE.DataModels.SimpleUML.Class tc = checkresult.matchDomainC.tc;

			// Contructing t

				// Return newly binded variables
								match.t  = t;
				return match;
		}

		public class CheckOnlyDomains : Tuple<Class,String>
		{
			public CheckOnlyDomains(Class c,String prefix)
				: base(c,prefix)
			{
			}

			public Class c
			{
				get { return Item1; }
			}

			public String prefix
			{
				get { return Item2; }
			}
		}

		public class EnforceDomains : Tuple<Table>
		{
			public EnforceDomains(Table t)
				: base(t)
			{
			}

			public Table t
			{
				get { return Item1; }
			}
		}

		internal class CheckResultComplexAttributeToColumn
		{
			public MatchDomainC matchDomainC;
		}

		internal class MatchDomainC
		{
			public LL.MDE.DataModels.SimpleUML.Attribute a;
			public string an;
			public LL.MDE.DataModels.SimpleUML.Class c;
			public LL.MDE.DataModels.SimpleUML.Class tc;
		}

		internal class MatchDomainT
		{
			public LL.MDE.DataModels.SimpleRDBMS.Table t;
		}
	}
}