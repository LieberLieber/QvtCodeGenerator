using System.Globalization;

using LL.MDE.Components.Qvt.Metamodel.QVTBase;
using LL.MDE.Components.Qvt.Metamodel.QVTRelation;
using LL.MDE.Components.Qvt.QvtCodeGenerator.Utils;

namespace LL.MDE.Components.Qvt.QvtCodeGenerator.CodeGeneration
{
    public static class QvtCodeGeneratorStrings
    {
        private static string ToFirstUpper(string s)
        {
            return s.Substring(0, 1).ToUpper() + s.Substring(1, s.Length - 1);
        }

        public static string Namespace(ITransformation transformation)
        {
            return "namespace LL.MDE.Components.Qvt.Transformation." + transformation.Name;
        }

        public static string RelationCheckTowards(IRelation relation, ITypedModel param)
        {
            return "CheckTowards" + ToFirstUpper(param.Name);
        }

        public static string RelationEnforceTowards(IRelation relation, ITypedModel param)
        {
            return "EnforceTowards" + ToFirstUpper(param.Name);
        }

        public static string GetFileName(IRelationalTransformation transformation)
        {
            return "Transformation" + transformation.Name + ".cs";
        }

        public static string GetFileNameFunctions(IRelationalTransformation transformation)
        {
            return FunctionsInterfaceName(transformation) +".cs";
        }

        public static string GetFileName(IRelation relation)
        {
            return "Relation" + relation.Name + ".cs";
        }

        public static string CheckResultClassName(IRelation relation)
        {
            return "CheckResult" + ToFirstUpper(relation.Name);
        }

        public static string MatchClassName(ITransformation transformation)
        {
            return "Match" + ToFirstUpper(transformation.Name);
        }

        public static string CheckTowardsMethodName(ITypedModel targetModel)
        {
            return "CheckTowards" + targetModel.Name;
        }

        public static string EnforceTowardsMethodName(ITypedModel targetModel)
        {
            return "EnforceTowards" + Util.ToFirstUpper(targetModel.Name);
        }

        public static string TypedModelVariableName(ITypedModel model)
        {
            return Util.ToFirstUpper(model.Name);
        }

        public static string SetParamMethodName(ITypedModel model)
        {
            return "SetParam" + Util.ToFirstUpper(model.Name);
        }

        public static string RelationMatchSourceTowards(IRelation relation, ITypedModel param)
        {
            return "MatchSourceTowards" + ToFirstUpper(param.Name);
        }

        public static string RelationMatchTargetTowards(IRelation relation, ITypedModel targetParam)
        {
            return "MatchTargetTowards" + ToFirstUpper(targetParam.Name);
        }

        public static string TransformationName(ITransformation transformation)
        {
            return "Transformation" + transformation.Name;
        }

        public static string RelationClassName(IRelation relation)
        {
            return "Relation" + relation.Name;
        }

        public static string MatchDomainClassName(IRelationDomain domain)
        {
            return Util.ToFirstUpper(MatchDomainFieldName(domain));
        }

        public static string MatchDomainFieldName(IRelationDomain domain)
        {
            return "matchDomain" + Util.ToFirstUpper(domain.RootVariable.Name);
        }

        public static object CheckDomainMethodName(IRelationDomain domain)
        {
            return "CheckDomain" + Util.ToFirstUpper(domain.RootVariable.Name);
        }

        public static string CheckMethodName()
        {
            return "Check";
        }

        public static string FunctionsInterfaceName(IRelationalTransformation transformation)
        {
            return "IFunctions";
        }

        public static string EnforceDomainMethodName(IRelationDomain targetDomain)
        {
            return "Enforce" + ToFirstUpper(targetDomain.RootVariable.Name);
        }

        public static string KeyDictionnaryName(IKey key)
        {
            return key.Identifies.Name + "Keys";
        }
    }
}