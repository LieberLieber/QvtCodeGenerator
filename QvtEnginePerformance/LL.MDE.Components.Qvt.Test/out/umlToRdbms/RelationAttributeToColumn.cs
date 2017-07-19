namespace LL.MDE.Components.Qvt.Transformation.umlToRdbms
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using LL.MDE.Components.Qvt.Common;
	using LL.MDE.DataModels.SimpleRDBMS;
	using LL.MDE.DataModels.SimpleUML;

	public class RelationAttributeToColumn
	{
		private readonly IMetaModelInterface editor;
		private readonly Dictionary<CheckOnlyDomains, EnforceDomains> traceabilityMap = new Dictionary<CheckOnlyDomains, EnforceDomains>();
		private readonly TransformationumlToRdbms transformation;

		public RelationAttributeToColumn(IMetaModelInterface editor , TransformationumlToRdbms transformation )
		{
			this.editor = editor;this.transformation = transformation;
		}

		public EnforceDomains FindPreviousResult(LL.MDE.DataModels.SimpleUML.Class c,string prefix)
		{
			CheckOnlyDomains input = new CheckOnlyDomains(c,prefix);
			return traceabilityMap.ContainsKey(input) ? traceabilityMap[input] : null;
		}

		internal static ISet<CheckResultAttributeToColumn> Check( LL.MDE.DataModels.SimpleUML.Class c,string prefix )
		{
			ISet<CheckResultAttributeToColumn> result = new HashSet<CheckResultAttributeToColumn>();ISet<MatchDomainC> matchDomainCs = CheckDomainC(c);
			foreach (MatchDomainC matchDomainC in matchDomainCs ) {

			CheckResultAttributeToColumn checkonlysMatch = new CheckResultAttributeToColumn () {matchDomainC = matchDomainC,
			};
			result.Add(checkonlysMatch);			} // End foreach
				return result;
		}

		internal static ISet<MatchDomainC> CheckDomainC(LL.MDE.DataModels.SimpleUML.Class c)
		{
			ISet<MatchDomainC> result = new HashSet<MatchDomainC>();
			if (c != null) {
			MatchDomainC match = new MatchDomainC() {
			c = c,
			};
			result.Add(match);
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
				ISet<CheckResultAttributeToColumn> result = Check (c,prefix);
				Enforce(result, t,prefix);
				traceabilityMap[input] = output;
			}
		}

		internal void Enforce(ISet<CheckResultAttributeToColumn> result, LL.MDE.DataModels.SimpleRDBMS.Table t,string prefix )
		{
			foreach (CheckResultAttributeToColumn match in result)
			{
			// Extracting variables binded in source domains
			LL.MDE.DataModels.SimpleUML.Class c = match.matchDomainC.c;

			// Assigning variables binded in the where clause

			// Enforcing each enforced domain
			MatchDomainT targetMatchDomainT = EnforceT(match, t);

			// Retrieving variables binded in the enforced domains

			// Calling other relations as defined in the where clause
			transformation.RelationPrimitiveAttributeToColumn.CheckAndEnforce(c,t,prefix);
				transformation.RelationComplexAttributeToColumn.CheckAndEnforce(c,t,prefix);
				transformation.RelationSuperAttributeToColumn.CheckAndEnforce(c,t,prefix);
				}
		}

		internal MatchDomainT EnforceT(CheckResultAttributeToColumn checkresult, LL.MDE.DataModels.SimpleRDBMS.Table t)
		{
			MatchDomainT match = new MatchDomainT();LL.MDE.DataModels.SimpleUML.Class c = checkresult.matchDomainC.c;

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

		internal class CheckResultAttributeToColumn
		{
			public MatchDomainC matchDomainC;
		}

		internal class MatchDomainC
		{
			public LL.MDE.DataModels.SimpleUML.Class c;
		}

		internal class MatchDomainT
		{
			public LL.MDE.DataModels.SimpleRDBMS.Table t;
		}
	}
}