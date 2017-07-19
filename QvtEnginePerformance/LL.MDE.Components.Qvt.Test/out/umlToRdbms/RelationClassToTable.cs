namespace LL.MDE.Components.Qvt.Transformation.umlToRdbms
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using LL.MDE.Components.Qvt.Common;
	using LL.MDE.DataModels.SimpleRDBMS;
	using LL.MDE.DataModels.SimpleUML;

	public class RelationClassToTable
	{
		private readonly IMetaModelInterface editor;
		private readonly Dictionary<CheckOnlyDomains, EnforceDomains> traceabilityMap = new Dictionary<CheckOnlyDomains, EnforceDomains>();
		private readonly TransformationumlToRdbms transformation;

		public RelationClassToTable(IMetaModelInterface editor , TransformationumlToRdbms transformation )
		{
			this.editor = editor;this.transformation = transformation;
		}

		public EnforceDomains FindPreviousResult(LL.MDE.DataModels.SimpleUML.Class c)
		{
			CheckOnlyDomains input = new CheckOnlyDomains(c);
			return traceabilityMap.ContainsKey(input) ? traceabilityMap[input] : null;
		}

		internal static ISet<CheckResultClassToTable> Check( LL.MDE.DataModels.SimpleUML.Class c )
		{
			ISet<CheckResultClassToTable> result = new HashSet<CheckResultClassToTable>();ISet<MatchDomainC> matchDomainCs = CheckDomainC(c);
			foreach (MatchDomainC matchDomainC in matchDomainCs ) {
			string cn = matchDomainC.cn;
			LL.MDE.DataModels.SimpleUML.Package p = matchDomainC.p;

			CheckResultClassToTable checkonlysMatch = new CheckResultClassToTable () {matchDomainC = matchDomainC,
			};
			result.Add(checkonlysMatch);			} // End foreach
				return result;
		}

		internal static ISet<MatchDomainC> CheckDomainC(LL.MDE.DataModels.SimpleUML.Class c)
		{
			ISet<MatchDomainC> result = new HashSet<MatchDomainC>();
			if (c != null) {
			string cn = (string)c.name;
			LL.MDE.DataModels.SimpleUML.Package p = (LL.MDE.DataModels.SimpleUML.Package)c.@namespace;
			if (c.kind == "persistent") {
			if (p != null) {
			MatchDomainC match = new MatchDomainC() {
			c = c,
			cn = cn,
			p = p,
			};
			result.Add(match);
			}
			}}

			return result;
		}

		internal void CheckAndEnforce(LL.MDE.DataModels.SimpleUML.Class c,LL.MDE.DataModels.SimpleRDBMS.Table t )
		{
			CheckOnlyDomains input = new CheckOnlyDomains(c);
			EnforceDomains output = new EnforceDomains(t);
			if (traceabilityMap.ContainsKey(input) && !traceabilityMap[input].Equals(output))
			{
				throw new Exception("This relation has already been used with different enforced parameters!");
			}
			if (!traceabilityMap.ContainsKey(input))
			{
				ISet<CheckResultClassToTable> result = Check (c);
				Enforce(result, t);
				traceabilityMap[input] = output;
			}
		}

		internal void Enforce(ISet<CheckResultClassToTable> result, LL.MDE.DataModels.SimpleRDBMS.Table t )
		{
			foreach (CheckResultClassToTable match in result)
			{
			// Extracting variables binded in source domains
			LL.MDE.DataModels.SimpleUML.Class c = match.matchDomainC.c;
			string cn = match.matchDomainC.cn;
			LL.MDE.DataModels.SimpleUML.Package p = match.matchDomainC.p;

			// Assigning variables binded in the where clause

			// Enforcing each enforced domain
			MatchDomainT targetMatchDomainT = EnforceT(match, cn,t);

			// Retrieving variables binded in the enforced domains
			LL.MDE.DataModels.SimpleRDBMS.Schema s = targetMatchDomainT.s;
			LL.MDE.DataModels.SimpleRDBMS.Key k = targetMatchDomainT.k;
			LL.MDE.DataModels.SimpleRDBMS.Column cl = targetMatchDomainT.cl;

			// Calling other relations as defined in the where clause
			transformation.RelationAttributeToColumn.CheckAndEnforce(c,t,"");
				}
		}

		internal MatchDomainT EnforceT(CheckResultClassToTable checkresult, string cn,LL.MDE.DataModels.SimpleRDBMS.Table t)
		{
			MatchDomainT match = new MatchDomainT();LL.MDE.DataModels.SimpleUML.Class c = checkresult.matchDomainC.c;
			LL.MDE.DataModels.SimpleUML.Package p = checkresult.matchDomainC.p;

					// Querying when relations and storing results //var RelationPackageToSchemaResult =  transformation.RelationPackageToSchema.FindPreviousResult(p) ;

			// Contructing t
			editor.AddOrSetInField(t, "name", cn );
			LL.MDE.DataModels.SimpleRDBMS.Schema s = null;
			s =  (LL.MDE.DataModels.SimpleRDBMS.Schema) editor.CreateNewObjectInField(t, "schema");

			LL.MDE.DataModels.SimpleRDBMS.Key k = null;
			k =  (LL.MDE.DataModels.SimpleRDBMS.Key) editor.CreateNewObjectInField(t, "key");

			// Contructing s

			// Contructing k
			editor.AddOrSetInField(k, "name", cn + "_pk" );
			LL.MDE.DataModels.SimpleRDBMS.Column cl = null;
			cl =  (LL.MDE.DataModels.SimpleRDBMS.Column) editor.CreateNewObjectInField(k, "column");

			// Contructing cl
			editor.AddOrSetInField(cl, "type", "NUMBER" );
			editor.AddOrSetInField(cl, "name", cn + "tid" );
			editor.AddOrSetInField(cl, "owner", t );
			// Setting cycling properties
			editor.AddOrSetInField(t, "column", cl );

				// Return newly binded variables
								match.t  = t;
								match.s  = s;
								match.k  = k;
								match.cl  = cl;
				return match;
		}

		public class CheckOnlyDomains : Tuple<Class>
		{
			public CheckOnlyDomains(Class c)
				: base(c)
			{
			}

			public Class c
			{
				get { return Item1; }
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

		internal class CheckResultClassToTable
		{
			public MatchDomainC matchDomainC;
		}

		internal class MatchDomainC
		{
			public LL.MDE.DataModels.SimpleUML.Class c;
			public string cn;
			public LL.MDE.DataModels.SimpleUML.Package p;
		}

		internal class MatchDomainT
		{
			public LL.MDE.DataModels.SimpleRDBMS.Column cl;
			public LL.MDE.DataModels.SimpleRDBMS.Key k;
			public LL.MDE.DataModels.SimpleRDBMS.Schema s;
			public LL.MDE.DataModels.SimpleRDBMS.Table t;
		}
	}
}