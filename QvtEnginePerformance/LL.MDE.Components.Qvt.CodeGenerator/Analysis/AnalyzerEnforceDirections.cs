using System.Collections.Generic;
using System.Linq;

using LL.MDE.Components.Qvt.Metamodel.EssentialOCL;
using LL.MDE.Components.Qvt.Metamodel.QVTBase;
using LL.MDE.Components.Qvt.Metamodel.QVTRelation;

namespace LL.MDE.Components.Qvt.QvtCodeGenerator.Analysis
{
	


	public class AnalyzerEnforceDirections
	{
		private static bool AreValidSources(IEnumerable<DomainVariablesBindingsResult> sourceDomainsResults)
		{
			IEnumerable<DomainVariablesBindingsResult> sourceDomainsResultsList = sourceDomainsResults as IList<DomainVariablesBindingsResult> ?? sourceDomainsResults.ToList();
			foreach (DomainVariablesBindingsResult sourceDomainVariablesBindingsResult in sourceDomainsResultsList)
			{
				// If a given source domain cannot bind all its variables by itself (ie. required variables)
				// then we have to look if the variables are provided by other domains
				if (sourceDomainVariablesBindingsResult.IPropertyTemplateItemToVariablesRequired.Any())
				{
					// Implements "(Variable required) => (Variable provided)"
					IEnumerable<DomainVariablesBindingsResult> otherSourceDomainsResults = sourceDomainsResultsList.Where(d => d != sourceDomainVariablesBindingsResult);
					ISet<IVariable> allProvidedVariables = new HashSet<IVariable>(otherSourceDomainsResults.SelectMany(r => r.VariablesItCanBind));
					bool allVariabledProvided = allProvidedVariables.IsSupersetOf(sourceDomainVariablesBindingsResult.IPropertyTemplateItemToVariablesRequired.Values.SelectMany(i=>i));
					if (!allVariabledProvided)
						return false;
				}
			}
			return true;
		}

		public static ISet<ITypedModel> AnalyzeRelation(IRelation relation, ISet<DomainVariablesBindingsResult> DomainVariablesBindingsResults)
		{
			ISet<ITypedModel> result = new HashSet<ITypedModel>();

			// Then depending on which domains are enforced and which variables can be binded, we find the possible directions
			foreach (ITypedModel typedModel in relation.Transformation.ModelParameter)
			{
				IEnumerable<IRelationDomain> enforcedDomainsOfThisType = relation.Domain.OfType<IRelationDomain>().Where(d => d.IsEnforceable.GetValueOrDefault() && d.TypedModel == typedModel);
				IEnumerable<IRelationDomain> sourceDomainsOfAnyType = relation.Domain.OfType<IRelationDomain>().Where(d => !enforcedDomainsOfThisType.Contains(d));

				IEnumerable<DomainVariablesBindingsResult> sourceDomainsOfAnyTypeResults = sourceDomainsOfAnyType.Select(d => DomainVariablesBindingsResults.Single(r => r.AnalyzedDomain == d));

				if (enforcedDomainsOfThisType.Any() && AreValidSources(sourceDomainsOfAnyTypeResults))
				{
					result.Add(typedModel);
				}
			}
			return result;
		}

		

		
	}
}