namespace LL.MDE.Components.Qvt.Transformation.Demo1
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using LL.MDE.Components.Qvt.Common;
	using LL.MDE.DataModels.EnAr;

	public class RelationRelation1
	{
		private readonly IMetaModelInterface editor;
		private readonly Dictionary<CheckOnlyDomains, EnforceDomains> traceabilityMap = new Dictionary<CheckOnlyDomains, EnforceDomains>();
		private readonly TransformationDemo1 transformation;

		public RelationRelation1(IMetaModelInterface editor , TransformationDemo1 transformation )
		{
			this.editor = editor;this.transformation = transformation;
		}

		public void CheckAndEnforce(LL.MDE.DataModels.EnAr.Package p,string someString,LL.MDE.DataModels.EnAr.Package p2,LL.MDE.DataModels.EnAr.Package po )
		{
			CheckOnlyDomains input = new CheckOnlyDomains(p,someString,p2);
			EnforceDomains output = new EnforceDomains(po);
			if (traceabilityMap.ContainsKey(input) && !traceabilityMap[input].Equals(output))
			{
				throw new Exception("This relation has already been used with different enforced parameters!");
			}
			if (!traceabilityMap.ContainsKey(input))
			{
				ISet<CheckResultRelation1> result = Check (p,someString,p2);
				Enforce(result, someString,po);
				traceabilityMap[input] = output;
			}
		}

		public EnforceDomains FindPreviousResult(LL.MDE.DataModels.EnAr.Package p,string someString,LL.MDE.DataModels.EnAr.Package p2)
		{
			CheckOnlyDomains input = new CheckOnlyDomains(p,someString,p2);
			return traceabilityMap.ContainsKey(input) ? traceabilityMap[input] : null;
		}

		internal static ISet<CheckResultRelation1> Check( LL.MDE.DataModels.EnAr.Package p,string someString,LL.MDE.DataModels.EnAr.Package p2 )
		{
			ISet<CheckResultRelation1> result = new HashSet<CheckResultRelation1>();ISet<MatchDomainP> matchDomainPs = CheckDomainP(p);
			ISet<MatchDomainP2> matchDomainP2s = CheckDomainP2(p2);
			foreach (MatchDomainP matchDomainP in matchDomainPs ) {
			foreach (MatchDomainP2 matchDomainP2 in matchDomainP2s ) {
			string s = matchDomainP.s;
			LL.MDE.DataModels.EnAr.Element pe = matchDomainP.pe;
			LL.MDE.DataModels.EnAr.Connector c = matchDomainP.c;
			int target = matchDomainP.target;
			int source = matchDomainP.source;

			if(p2.Name == (s + "Other")) {CheckResultRelation1 checkonlysMatch = new CheckResultRelation1 () {matchDomainP = matchDomainP,
			matchDomainP2 = matchDomainP2,
			};
			result.Add(checkonlysMatch);			} // End if
						} // End foreach
						} // End foreach
				return result;
		}

		internal static ISet<MatchDomainP> CheckDomainP(LL.MDE.DataModels.EnAr.Package p)
		{
			ISet<MatchDomainP> result = new HashSet<MatchDomainP>();
			if (p != null) {
			string s = (string)p.Name;
			LL.MDE.DataModels.EnAr.Element pe = (LL.MDE.DataModels.EnAr.Element)p.Element;
			if (pe != null) {
			if (pe.Stereotype == "st1") {
			foreach (LL.MDE.DataModels.EnAr.Connector c  in pe.Connectors.OfType<LL.MDE.DataModels.EnAr.Connector>()) {
			if (c != null) {
			int target = (int)c.SupplierID;
			int source = (int)c.ClientID;
			MatchDomainP match = new MatchDomainP() {
			p = p,
			s = s,
			pe = pe,
			c = c,
			target = target,
			source = source,
			};
			result.Add(match);
			}
			}
			}}
			}

			return result;
		}

		internal static ISet<MatchDomainP2> CheckDomainP2(LL.MDE.DataModels.EnAr.Package p2)
		{
			ISet<MatchDomainP2> result = new HashSet<MatchDomainP2>();
			if (p2 != null) {
			MatchDomainP2 match = new MatchDomainP2() {
			p2 = p2,
			};
			result.Add(match);
			}

			return result;
		}

		internal void Enforce(ISet<CheckResultRelation1> result, string someString,LL.MDE.DataModels.EnAr.Package po )
		{
			foreach (CheckResultRelation1 match in result)
			{
			// Extracting variables binded in source domains
			LL.MDE.DataModels.EnAr.Package p = match.matchDomainP.p;
			string s = match.matchDomainP.s;
			LL.MDE.DataModels.EnAr.Element pe = match.matchDomainP.pe;
			LL.MDE.DataModels.EnAr.Connector c = match.matchDomainP.c;
			int target = match.matchDomainP.target;
			int source = match.matchDomainP.source;

			LL.MDE.DataModels.EnAr.Package p2 = match.matchDomainP2.p2;

			// Enforcing each enforced domain
			MatchDomainPo targetMatchDomainPo = EnforcePo(match, s,someString,source,po);

			// Retrieving variables binded in the enforced domains
			LL.MDE.DataModels.EnAr.Element e = targetMatchDomainPo.e;
			LL.MDE.DataModels.EnAr.Connector con = targetMatchDomainPo.con;

			}
		}

		internal MatchDomainPo EnforcePo(CheckResultRelation1 checkresult, string s,string someString,int source,LL.MDE.DataModels.EnAr.Package po)
		{
			MatchDomainPo match = new MatchDomainPo();LL.MDE.DataModels.EnAr.Package p = checkresult.matchDomainP.p;
			LL.MDE.DataModels.EnAr.Element pe = checkresult.matchDomainP.pe;
			LL.MDE.DataModels.EnAr.Connector c = checkresult.matchDomainP.c;

					LL.MDE.DataModels.EnAr.Package p2 = checkresult.matchDomainP2.p2;

			// Contructing po
			editor.AddOrSetInField(po, "Name", s + "Out" );
			LL.MDE.DataModels.EnAr.Element e = null;

			// Trying to resolve the object'e' globally using the transformation key
			transformation.ElementKeys.TryGetValue(new Tuple<string>(s + someString), out e);
			// If the object wasn't found globally, we try to find it locally
			if (e== null) {
			e = po.Elements.OfType<LL.MDE.DataModels.EnAr.Element>().FirstOrDefault(var865311109 => var865311109?.Name == s + someString);

			// If the object was found locally, we add it to the global cache
			if (e!= null) {
			transformation.ElementKeys[new Tuple<string>(e?.Name)] = e;
			}
			// If the object still doesn't exist, we create it
			else {
			e =  (LL.MDE.DataModels.EnAr.Element) editor.CreateNewObjectInField(po, "Elements");
			// We add the created object to the global cache
			if (transformation.ElementKeys.ContainsKey(new Tuple<string>(e?.Name))) {
			throw new Exception("Two objects cannot have the same key");
			} else {
			transformation.ElementKeys[new Tuple<string>(e?.Name)]=e;
			}

			}
			}

			// Contructing e
			editor.AddOrSetInField(e, "Name", s + someString );
			editor.AddOrSetInField(e, "Type", "Component" );
			LL.MDE.DataModels.EnAr.Connector con = null;
			con =  (LL.MDE.DataModels.EnAr.Connector) editor.CreateNewObjectInField(e, "Connectors");

			// Contructing con
			editor.AddOrSetInField(con, "SupplierID", source );
			editor.AddOrSetInField(con, "Type", "Dependency" );

				// Return newly binded variables
								match.po  = po;
								match.e  = e;
								match.con  = con;
				return match;
		}

		public class CheckOnlyDomains : Tuple<Package,string,Package>
		{
			public CheckOnlyDomains(Package p,string someString,Package p2)
				: base(p,someString,p2)
			{
			}

			public Package p
			{
				get { return Item1; }
			}

			public Package p2
			{
				get { return Item3; }
			}

			public string someString
			{
				get { return Item2; }
			}
		}

		public class EnforceDomains : Tuple<Package>
		{
			public EnforceDomains(Package po)
				: base(po)
			{
			}

			public Package po
			{
				get { return Item1; }
			}
		}

		internal class CheckResultRelation1
		{
			public MatchDomainP matchDomainP;
			public MatchDomainP2 matchDomainP2;
		}

		internal class MatchDomainP
		{
			public LL.MDE.DataModels.EnAr.Connector c;
			public LL.MDE.DataModels.EnAr.Package p;
			public LL.MDE.DataModels.EnAr.Element pe;
			public string s;
			public int source;
			public int target;
		}

		internal class MatchDomainP2
		{
			public LL.MDE.DataModels.EnAr.Package p2;
		}

		internal class MatchDomainPo
		{
			public LL.MDE.DataModels.EnAr.Connector con;
			public LL.MDE.DataModels.EnAr.Element e;
			public LL.MDE.DataModels.EnAr.Package po;
		}
	}
}