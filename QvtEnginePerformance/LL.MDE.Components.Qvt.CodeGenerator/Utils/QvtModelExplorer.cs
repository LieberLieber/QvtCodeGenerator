using System.Collections.Generic;
using System.Linq;

using LL.MDE.Components.Qvt.Metamodel.CustomExtensions.EMOFExtensions;
using LL.MDE.Components.Qvt.Metamodel.EssentialOCL;
using LL.MDE.Components.Qvt.Metamodel.QVTRelation;
using LL.MDE.Components.Qvt.Metamodel.QVTTemplate;

namespace LL.MDE.Components.Qvt.QvtCodeGenerator.Utils
{
	public class QvtModelExplorer
	{
		public static List<IObjectTemplateExp> FindAllObjectTemplates(IEnumerable<IRelationDomain> domains)
		{
			List<IObjectTemplateExp> result = new List<IObjectTemplateExp>();
			foreach (IRelationDomain domain in domains)
			{
				result.AddRange(FindAllObjectTemplates(domain));
			}
			return result;
		}

		public static List<IObjectTemplateExp> FindAllObjectTemplates(IRelationDomain domain)
		{
		    IObjectTemplateExp domainTemplate = domain.Pattern?.TemplateExpression as IObjectTemplateExp;
		    if (domainTemplate != null)
		    {
		        return FindAllObjectTemplates(domainTemplate);
		    }
		    return new List<IObjectTemplateExp>();
		}

		private static void FindAllObjectTemplates(IObjectTemplateExp template, ICollection<IObjectTemplateExp> foundSoFar)
		{
			foundSoFar.Add(template);
			foreach (IObjectTemplateExp subTemplate in template.Part.Select(prop => prop.Value).OfType<IObjectTemplateExp>().Where(t => !foundSoFar.Contains(t)))
			{
				foreach (IObjectTemplateExp res in FindAllObjectTemplates(subTemplate))
				{
					foundSoFar.Add(res);
				}
			}
		}

		public static List<IObjectTemplateExp> FindAllObjectTemplates(IObjectTemplateExp template)
		{
			List<IObjectTemplateExp> result = new List<IObjectTemplateExp>();
			FindAllObjectTemplates(template, result);
			return result;
		}

		public static List<IPropertyTemplateItem> FindAllPropertyTemplates(IRelationDomain domain)
		{
			List<IPropertyTemplateItem> result = new List<IPropertyTemplateItem>();
			foreach (IObjectTemplateExp objectTemplateExp in FindAllObjectTemplates(domain))
			{
				result.AddRange(objectTemplateExp.Part);
			}
			return result;
		}

		public static ISet<IVariable> FindAllVariables(IRelationDomain domain, ISet<IVariable> bindedVariables)
		{
			ISet<IVariable> result = new HashSet<IVariable>();
			result.Add(domain.RootVariable);
			bindedVariables.Add(domain.RootVariable);
			foreach (IPropertyTemplateItem prop in FindAllPropertyTemplates(domain))
			{
				if (prop.Value is IVariableExp)
				{
					IVariableExp cast = (IVariableExp)prop.Value;
					result.Add(cast.ReferredVariable);
					bindedVariables.Add(cast.ReferredVariable);
				}
				else if (prop.Value is IObjectTemplateExp)
				{
					IObjectTemplateExp cast = (IObjectTemplateExp)prop.Value;
					result.Add(cast.BindsTo);
					bindedVariables.Add(cast.BindsTo);
				}
				else if (prop.Value is CSharpOpaqueExpression)
				{
					CSharpOpaqueExpression cast = (CSharpOpaqueExpression)prop.Value;
					result.UnionWith(cast.BindsTo);
				}
			}

			return result;
		}

		public static ISet<IVariable> FindAllVariables(IEnumerable<IRelationDomain> domains)
		{
			ISet<IVariable> result = new HashSet<IVariable>();
			foreach (IRelationDomain relationDomain in domains)
			{
				result.UnionWith(FindAllVariables(relationDomain));
			}
			return new HashSet<IVariable>(result.Where(v => v != null));
		}

		public static ISet<IVariable> FindAllVariables(IRelationDomain domain)
		{
			ISet<IVariable> binded = new HashSet<IVariable>();
			return FindAllVariables(domain, binded);
		}

		public static ISet<IVariable> FindBindedVariables(IPropertyTemplateItem prop)
		{
			if (!prop.ReferredProperty.isMany())
			{
				IObjectTemplateExp nonManyPropObjectTemplate = prop.Value as IObjectTemplateExp;
				if (nonManyPropObjectTemplate != null)
				{
					return new HashSet<IVariable>() { nonManyPropObjectTemplate.BindsTo };
				}
				else
				{
					IVariableExp nonManyPropVar = prop.Value as IVariableExp;
					if (nonManyPropVar != null)
					{
						return new HashSet<IVariable>() { nonManyPropVar.ReferredVariable };
					}
					else
					{
						CSharpOpaqueExpression cast = prop.Value as CSharpOpaqueExpression;
						if (cast != null)
						{
							return new HashSet<IVariable>(cast.BindsTo);
						}
					}
				}
			}
			return new HashSet<IVariable>();
		}

 
		/*public static ICollection<IVariable> FindVariablesBindedIn(IRelation relation, List<IRelationDomain> sourceDomains)
		{
			ISet<IVariable> result = new HashSet<IVariable>();

			foreach (IRelationDomain relationDomain in sourceDomains)
			{
				result.Add(relationDomain.RootVariable);
				foreach (var VARIABLE in relationDomain.Pattern.)
				{
					
				}
			}

			return result;
		}*/
	}
}