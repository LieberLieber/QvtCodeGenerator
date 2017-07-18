using System.Collections.Generic;
using System.Linq;

using LL.MDE.Components.Qvt.Metamodel.QVTBase;
using LL.MDE.Components.Qvt.Metamodel.QVTRelation;

using NMF.Utilities;

namespace LL.MDE.Components.Qvt.QvtCodeGenerator.Analysis
{
	public class RelationAnalysisResult
	{
		public IRelation AnalyzedRelation;

		public readonly ISet<ITypedModel> DirectionsThatCanBeEnforced = new HashSet<ITypedModel>();
		public readonly ISet<DomainVariablesBindingsResult> DomainAnalysisResults = new HashSet<DomainVariablesBindingsResult>();
        //public readonly ISet<IKey> EffectiveKeys = new HashSet<IKey>();

		public DomainVariablesBindingsResult GetResultOf(IRelationDomain domain)
		{
			return DomainAnalysisResults.Single(r => r.AnalyzedDomain == domain);
		}
	}

	public class AnalysersBatchHelper
	{
		public static RelationAnalysisResult AnalyzeRelation(IRelation relation)
		{
			RelationAnalysisResult result = new RelationAnalysisResult { AnalyzedRelation = relation };

			// First we analyze all the domains of the relation
			foreach (IRelationDomain domain in relation.Domain.OfType<IRelationDomain>())
			{
				DomainVariablesBindingsResult domainResult = AnalyzerVariablesBindings.AnalyzeDomain(domain);
				result.DomainAnalysisResults.Add(domainResult);
			}

			ISet<ITypedModel> otherresult = AnalyzerEnforceDirections.AnalyzeRelation(relation, result.DomainAnalysisResults);
			result.DirectionsThatCanBeEnforced.UnionWith(otherresult);

            //result.EffectiveKeys.AddRange(AnalyzerEffectiveKeys.Run(relation));

            return result;
		}
	}
}