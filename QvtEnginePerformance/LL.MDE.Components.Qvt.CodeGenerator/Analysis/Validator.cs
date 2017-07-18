using System.Collections.Generic;
using System.Linq;

using LL.MDE.Components.Qvt.Metamodel.EssentialOCL;
using LL.MDE.Components.Qvt.Metamodel.QVTRelation;
using LL.MDE.Components.Qvt.QvtCodeGenerator.Utils;

namespace LL.MDE.Components.Qvt.QvtCodeGenerator.Analysis
{
    public class Validator
    {
        public static bool IsValidSourceDomain(IRelationDomain domain)
        {
            ISet<IVariable> bindedVariables = new HashSet<IVariable>();
            ISet<IVariable> variables = QvtModelExplorer.FindAllVariables(domain, bindedVariables);
            // The domain is valid if all variables are directly binded in the pattern
            return (variables.All(v => bindedVariables.Contains(v)));
        }

        public static bool IsValidTargetDomain(IRelationDomain domain)
        {
            return ((IRelation)domain.Rule).Domain.Where(source => source != domain).ToList().TrueForAll
                (d => IsValidSourceDomain((IRelationDomain)d));
        }

        public static bool AreValidTargetDomains(ICollection<IRelationDomain> domains)
        {
            if (domains.Any())
            {
                IRelation rule = (IRelation)domains.First().Rule;
                return rule.Domain.Where(source => !domains.Contains(source)).ToList().TrueForAll
                    (d => IsValidSourceDomain((IRelationDomain)d));
            }
            else
            {
                return false;
            }
        }

        public static ISet<IRelationDomain> GetValidSourcesOfDomain(IRelationDomain domain)
        {
            HashSet<IRelationDomain> result = new HashSet<IRelationDomain>();
            result.UnionWith(((IRelation)domain.Rule).Domain.Cast<IRelationDomain>().Where(source => source != domain && IsValidSourceDomain(source)));
            return result;
        }

        public static IEnumerable<IRelationDomain> GetValidSourcesOfDomains(IEnumerable<IRelationDomain> targetDomains)
        {
            IEnumerable<IRelationDomain> relationDomains = targetDomains as IList<IRelationDomain> ?? targetDomains.ToList();
            IRelation rule = (IRelation)relationDomains.First().Rule;
            IEnumerable<IRelationDomain> result = rule.Domain.Where(source => !relationDomains.Contains(source)).Cast<IRelationDomain>().Where
                (IsValidSourceDomain);
            return result;
        }
    }
}