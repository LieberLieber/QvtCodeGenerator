namespace LL.MDE.Components.Qvt.Transformation.umlToRdbms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using LL.MDE.Components.Qvt.Common;
    using LL.MDE.DataModels.SimpleRDBMS;
    using LL.MDE.DataModels.SimpleUML;

    public class RelationAssocToFKey
    {
        private readonly IMetaModelInterface editor;
        private readonly Dictionary<CheckOnlyDomains, EnforceDomains> traceabilityMap = new Dictionary<CheckOnlyDomains, EnforceDomains>();
        private readonly TransformationumlToRdbms transformation;

        public RelationAssocToFKey(IMetaModelInterface editor, TransformationumlToRdbms transformation)
        {
            this.editor = editor;
            this.transformation = transformation;
        }

        public EnforceDomains FindPreviousResult(LL.MDE.DataModels.SimpleUML.Association a)
        {
            CheckOnlyDomains input = new CheckOnlyDomains(a);
            return traceabilityMap.ContainsKey(input) ? traceabilityMap[input] : null;
        }

        internal static ISet<CheckResultAssocToFKey> Check(LL.MDE.DataModels.SimpleUML.Association a)
        {
            ISet<CheckResultAssocToFKey> result = new HashSet<CheckResultAssocToFKey>();
            ISet<MatchDomainA> matchDomainAs = CheckDomainA(a);
            foreach (MatchDomainA matchDomainA in matchDomainAs)
            {
                string an = matchDomainA.an;
                LL.MDE.DataModels.SimpleUML.Package p = matchDomainA.p;
                LL.MDE.DataModels.SimpleUML.Class sc = matchDomainA.sc;
                string scn = matchDomainA.scn;
                LL.MDE.DataModels.SimpleUML.Class dc = matchDomainA.dc;
                string dcn = matchDomainA.dcn;

                CheckResultAssocToFKey checkonlysMatch = new CheckResultAssocToFKey()
                {
                    matchDomainA = matchDomainA,
                };
                result.Add(checkonlysMatch);
            } // End foreach
            return result;
        }

        internal static ISet<MatchDomainA> CheckDomainA(LL.MDE.DataModels.SimpleUML.Association a)
        {
            ISet<MatchDomainA> result = new HashSet<MatchDomainA>();
            if (a != null)
            {
                string an = (string)a.name;
                LL.MDE.DataModels.SimpleUML.Package p = (LL.MDE.DataModels.SimpleUML.Package)a.@namespace;
                LL.MDE.DataModels.SimpleUML.Class sc = (LL.MDE.DataModels.SimpleUML.Class)a.source;
                LL.MDE.DataModels.SimpleUML.Class dc = (LL.MDE.DataModels.SimpleUML.Class)a.destination;
                if (p != null)
                {
                    if (sc != null)
                    {
                        string scn = (string)sc.name;
                        if (dc != null)
                        {
                            string dcn = (string)dc.name;
                            MatchDomainA match = new MatchDomainA()
                            {
                                a = a,
                                an = an,
                                p = p,
                                sc = sc,
                                scn = scn,
                                dc = dc,
                                dcn = dcn,
                            };
                            result.Add(match);
                        }
                    }
                }
            }

            return result;
        }

        internal void CheckAndEnforce(LL.MDE.DataModels.SimpleUML.Association a, LL.MDE.DataModels.SimpleRDBMS.ForeignKey fk)
        {
            CheckOnlyDomains input = new CheckOnlyDomains(a);
            EnforceDomains output = new EnforceDomains(fk);
            if (traceabilityMap.ContainsKey(input) && !traceabilityMap[input].Equals(output))
            {
                throw new Exception("This relation has already been used with different enforced parameters!");
            }
            if (!traceabilityMap.ContainsKey(input))
            {
                ISet<CheckResultAssocToFKey> result = Check(a);
                Enforce(result, fk);
                traceabilityMap[input] = output;
            }
        }

        internal void Enforce(ISet<CheckResultAssocToFKey> result, LL.MDE.DataModels.SimpleRDBMS.ForeignKey fk)
        {
            foreach (CheckResultAssocToFKey match in result)
            {
                // Extracting variables binded in source domains
                LL.MDE.DataModels.SimpleUML.Association a = match.matchDomainA.a;
                string an = match.matchDomainA.an;
                LL.MDE.DataModels.SimpleUML.Package p = match.matchDomainA.p;
                LL.MDE.DataModels.SimpleUML.Class sc = match.matchDomainA.sc;
                string scn = match.matchDomainA.scn;
                LL.MDE.DataModels.SimpleUML.Class dc = match.matchDomainA.dc;
                string dcn = match.matchDomainA.dcn;

                // Assigning variables binded in the where clause
                string fkn = scn + "_" + an + "_" + dcn;
                string fcn = fkn + "_tid";

                // Enforcing each enforced domain
                MatchDomainFk targetMatchDomainFk = EnforceFk(match, fkn, fcn, fk);

                // Retrieving variables binded in the enforced domains
                LL.MDE.DataModels.SimpleRDBMS.Table srcTbl = targetMatchDomainFk.srcTbl;
                LL.MDE.DataModels.SimpleRDBMS.Schema s = targetMatchDomainFk.s;
                LL.MDE.DataModels.SimpleRDBMS.Key pkey = targetMatchDomainFk.pkey;
                LL.MDE.DataModels.SimpleRDBMS.Table destTbl = targetMatchDomainFk.destTbl;
                LL.MDE.DataModels.SimpleRDBMS.Column cl = targetMatchDomainFk.cl;

                // Calling other relations as defined in the where clause
            }
        }

        internal MatchDomainFk EnforceFk(CheckResultAssocToFKey checkresult, string fkn, string fcn, LL.MDE.DataModels.SimpleRDBMS.ForeignKey fk)
        {
            MatchDomainFk match = new MatchDomainFk();
            LL.MDE.DataModels.SimpleUML.Association a = checkresult.matchDomainA.a;
            LL.MDE.DataModels.SimpleUML.Package p = checkresult.matchDomainA.p;
            LL.MDE.DataModels.SimpleUML.Class sc = checkresult.matchDomainA.sc;
            LL.MDE.DataModels.SimpleUML.Class dc = checkresult.matchDomainA.dc;

            // Querying when relations and storing results 
            LL.MDE.DataModels.SimpleRDBMS.Schema s = transformation.RelationPackageToSchema.FindPreviousResult(p).s;
            LL.MDE.DataModels.SimpleRDBMS.Table srcTbl = transformation.RelationClassToTable.FindPreviousResult(sc).t;
            LL.MDE.DataModels.SimpleRDBMS.Table destTbl = transformation.RelationClassToTable.FindPreviousResult(dc).t;

            // Contructing fk
            editor.AddOrSetInField(fk, "name", fkn);
            //LL.MDE.DataModels.SimpleRDBMS.Table srcTbl = null;
           // srcTbl = (LL.MDE.DataModels.SimpleRDBMS.Table)editor.CreateNewObjectInField(fk, "owner");

            LL.MDE.DataModels.SimpleRDBMS.Key pkey = null;
            pkey = (LL.MDE.DataModels.SimpleRDBMS.Key)editor.CreateNewObjectInField(fk, "key");

            LL.MDE.DataModels.SimpleRDBMS.Column cl = null;
            cl = (LL.MDE.DataModels.SimpleRDBMS.Column)editor.CreateNewObjectInField(fk, "column");

            // Contructing srcTbl
          //  LL.MDE.DataModels.SimpleRDBMS.Schema s = null;
         //   s = (LL.MDE.DataModels.SimpleRDBMS.Schema)editor.CreateNewObjectInField(srcTbl, "schema");

            // Contructing s

            // Contructing pkey
           // LL.MDE.DataModels.SimpleRDBMS.Table destTbl = null;
          //  destTbl = (LL.MDE.DataModels.SimpleRDBMS.Table)editor.CreateNewObjectInField(pkey, "owner");

            // Contructing destTbl

            // Contructing cl
            editor.AddOrSetInField(cl, "type", "NUMBER");
            editor.AddOrSetInField(cl, "name", fcn);

            // Return newly binded variables
            match.fk = fk;
            match.srcTbl = srcTbl;
            match.s = s;
            match.pkey = pkey;
            match.destTbl = destTbl;
            match.cl = cl;
            return match;
        }

        public class CheckOnlyDomains : Tuple<Association>
        {
            public CheckOnlyDomains(Association a)
                : base(a)
            {
            }

            public Association a
            {
                get { return Item1; }
            }
        }

        public class EnforceDomains : Tuple<ForeignKey>
        {
            public EnforceDomains(ForeignKey fk)
                : base(fk)
            {
            }

            public ForeignKey fk
            {
                get { return Item1; }
            }
        }

        internal class CheckResultAssocToFKey
        {
            public MatchDomainA matchDomainA;
        }

        internal class MatchDomainA
        {
            public LL.MDE.DataModels.SimpleUML.Association a;
            public string an;
            public LL.MDE.DataModels.SimpleUML.Class dc;
            public string dcn;
            public LL.MDE.DataModels.SimpleUML.Package p;
            public LL.MDE.DataModels.SimpleUML.Class sc;
            public string scn;
        }

        internal class MatchDomainFk
        {
            public LL.MDE.DataModels.SimpleRDBMS.Column cl;
            public LL.MDE.DataModels.SimpleRDBMS.Table destTbl;
            public LL.MDE.DataModels.SimpleRDBMS.ForeignKey fk;
            public LL.MDE.DataModels.SimpleRDBMS.Key pkey;
            public LL.MDE.DataModels.SimpleRDBMS.Schema s;
            public LL.MDE.DataModels.SimpleRDBMS.Table srcTbl;
        }
    }
}