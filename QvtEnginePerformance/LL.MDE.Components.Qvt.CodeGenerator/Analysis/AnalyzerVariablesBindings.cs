using System.Collections.Generic;
using System.Linq;

using LL.MDE.Components.Qvt.Metamodel.CustomExtensions.QVTTemplateExtensions;
using LL.MDE.Components.Qvt.Metamodel.EssentialOCL;
using LL.MDE.Components.Qvt.Metamodel.QVTRelation;
using LL.MDE.Components.Qvt.Metamodel.QVTTemplate;

using NMF.Utilities;

namespace LL.MDE.Components.Qvt.QvtCodeGenerator.Analysis
{
    public class DomainVariablesBindingsResult
    {
        public IRelationDomain AnalyzedDomain;

        public readonly ISet<IVariable> VariablesItCanBind = new HashSet<IVariable>();
        public readonly IDictionary<IPropertyTemplateItem, ISet<IVariable>> IPropertyTemplateItemToVariablesRequired = new Dictionary<IPropertyTemplateItem, ISet<IVariable>>();

        public ISet<IVariable> VariablesRequired()
        {
            return new HashSet<IVariable>(IPropertyTemplateItemToVariablesRequired.Values.SelectMany(i => i));
        }
    }

    public class AnalyzerVariablesBindings
    {
        public static DomainVariablesBindingsResult AnalyzeDomain(IRelationDomain domain)
        {
            DomainVariablesBindingsResult result = new DomainVariablesBindingsResult { AnalyzedDomain = domain };
            result.VariablesItCanBind.Add(domain.RootVariable);
            if (domain.Pattern != null)
            {
                ObjectTemplateExp objectTemplateExp = (ObjectTemplateExp)domain.Pattern.TemplateExpression;
                AnalyzeObjectTemplateExpression(objectTemplateExp, result, domain.IsEnforceable.GetValueOrDefault());
            }
            // if we can be self provided with a variable, we don't require it
            HashSet<KeyValuePair<IPropertyTemplateItem, ISet<IVariable>>> toRemove = new HashSet<KeyValuePair<IPropertyTemplateItem, ISet<IVariable>>>();
            foreach (KeyValuePair<IPropertyTemplateItem, ISet<IVariable>> required in result.IPropertyTemplateItemToVariablesRequired)
            {
                required.Value.ExceptWith(result.VariablesItCanBind);
                if (required.Value.Count == 0)
                {
                    toRemove.Add(required);
                }
            }
            result.IPropertyTemplateItemToVariablesRequired.RemoveRange(toRemove);

            return result;
        }

        private static void AnalyzeObjectTemplateExpression(IObjectTemplateExp objectTemplateExp, DomainVariablesBindingsResult currentResult, bool enforce, ISet<IObjectTemplateExp> analyzedSoFar = null)
        {
            if (analyzedSoFar == null)
                analyzedSoFar = new HashSet<IObjectTemplateExp>();

            if (!analyzedSoFar.Contains(objectTemplateExp))
            {
                analyzedSoFar.Add(objectTemplateExp);
                if (!objectTemplateExp.IsAntiTemplate())
                {
                    currentResult.VariablesItCanBind.Add(objectTemplateExp.BindsTo);
                }
                foreach (IPropertyTemplateItem propertyTemplateItem in objectTemplateExp.Part)
                {
                    if (propertyTemplateItem.Value is IObjectTemplateExp)
                    {
                        IObjectTemplateExp casted = (IObjectTemplateExp)propertyTemplateItem.Value;
                        AnalyzeObjectTemplateExpression(casted, currentResult, enforce, analyzedSoFar);
                    }
                    else if (propertyTemplateItem.Value is IVariableExp)
                    {
                        IVariableExp casted = (IVariableExp)propertyTemplateItem.Value;
                        if (enforce)
                        {
                            if (!currentResult.IPropertyTemplateItemToVariablesRequired.ContainsKey(propertyTemplateItem))
                                currentResult.IPropertyTemplateItemToVariablesRequired[propertyTemplateItem] = new HashSet<IVariable>();
                            currentResult.IPropertyTemplateItemToVariablesRequired[propertyTemplateItem].Add(casted.ReferredVariable);
                        }
                        else
                        {
                            currentResult.VariablesItCanBind.Add(casted.ReferredVariable);
                        }
                    }
                    else if (propertyTemplateItem.Value is CSharpOpaqueExpression)
                    {
                        CSharpOpaqueExpression casted = (CSharpOpaqueExpression)propertyTemplateItem.Value;
                        if (!currentResult.IPropertyTemplateItemToVariablesRequired.ContainsKey(propertyTemplateItem))
                            currentResult.IPropertyTemplateItemToVariablesRequired[propertyTemplateItem] = new HashSet<IVariable>();
                        currentResult.IPropertyTemplateItemToVariablesRequired[propertyTemplateItem].UnionWith(casted.BindsTo);
                    }
                }
            }
        }
    }
}