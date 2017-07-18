using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LL.MDE.Components.Qvt.Common;
using LL.MDE.Components.Qvt.Metamodel.CustomExtensions.EMOFExtensions;
using LL.MDE.Components.Qvt.Metamodel.CustomExtensions.QVTRelationExtensions;
using LL.MDE.Components.Qvt.Metamodel.CustomExtensions.QVTTemplateExtensions;
using LL.MDE.Components.Qvt.Metamodel.EMOF;
using LL.MDE.Components.Qvt.Metamodel.EssentialOCL;
using LL.MDE.Components.Qvt.Metamodel.QVTRelation;
using LL.MDE.Components.Qvt.Metamodel.QVTTemplate;
using LL.MDE.Components.Qvt.QvtCodeGenerator.Analysis;
using LL.MDE.Components.Qvt.QvtCodeGenerator.Utils;

using NMF.Utilities;

namespace LL.MDE.Components.Qvt.QvtCodeGenerator.CodeGeneration.RelationTemplate
{
    public class RelationTemplateHelper

    {
        public static string GenerateRelationParams(bool withTypes, IRelation relation, ISet<IVariable> outBindedVariables = null, bool checkonly = true, bool enforce = true, bool primitive = true)
        {
            IList<IVariable> variables = relation.Domain.Cast<RelationDomain>()
                .Where(d => d.IsEnforceable.GetValueOrDefault() == enforce
                            || !d.IsEnforceable.GetValueOrDefault() == checkonly
                            || (d.TypedModel == null) == primitive).Select(d => d.RootVariable).ToList();
            outBindedVariables?.UnionWith(new HashSet<IVariable>(variables));
            return string.Join(",", variables.Select(d => (withTypes ? d.Type.GetRealTypeName() + " " : "") + d.Name));
        }

        public static string GenerateRelationParamsCheckonly(bool withTypes, IRelation relation, ISet<IVariable> outBindedVariables = null)
        {
            return GenerateRelationParams(withTypes, relation, outBindedVariables, true, false);
        }

        public static string GenerateRelationParamsEnforce(bool withTypes, IRelation relation, ISet<IVariable> outBindedVariables = null)
        {
            return GenerateRelationParams(withTypes, relation, outBindedVariables, false);
        }

        public static string GenerateBindingFreeNonMany(IPropertyTemplateItem prop, IVariable bindedVariable)
        {
            return bindedVariable.Type.GetRealTypeName() + " " + bindedVariable.Name + " = (" + bindedVariable.Type.GetRealTypeName() + ")" + prop.ObjContainer.BindsTo.Name + "." + prop.ReferredProperty.Name + ";";
        }

        public static string GenerateDomainCheckMethodContent(IRelationDomain sourceDomain, ISet<IVariable> variablesBindedSoFar, DomainVariablesBindingsResult analysis, List<IObjectTemplateExp> remaining = null, StringBuilder stringBuilder = null, ISet<IPropertyTemplateItem> postPonedPropertiesToCheck = null)
        {
            if (remaining == null)
                remaining = QvtModelExplorer.FindAllObjectTemplates(sourceDomain).Where(o => !o.IsAntiTemplate()).ToList();
            if (stringBuilder == null)
                stringBuilder = new StringBuilder();
            if (postPonedPropertiesToCheck == null)
                postPonedPropertiesToCheck = new HashSet<IPropertyTemplateItem>();

            if (remaining.Count > 0)
            {
                IObjectTemplateExp current = remaining[0];
                remaining.RemoveAt(0);
                string currentVariableName = current.BindsTo.Name;
                variablesBindedSoFar.Add(current.BindsTo);

                // Generate conditional for the object template
                stringBuilder.AppendLine("if (" + currentVariableName + " != null) {");

                // Generate bindings for each non-many free variables 
                ISet<IPropertyTemplateItem> managedProps = new HashSet<IPropertyTemplateItem>();
                foreach (IPropertyTemplateItem nonManyProp in current.Part.Where(prop =>
                    (prop.Value is ObjectTemplateExp || prop.Value is VariableExp) &&
                    !prop.ReferredProperty.isMany()))
                {
                    IVariable bindedVariable = QvtModelExplorer.FindBindedVariables(nonManyProp).Single();
                    if (bindedVariable != null && !variablesBindedSoFar.Contains(bindedVariable))
                    {
                        managedProps.Add(nonManyProp);
                        stringBuilder.AppendLine(GenerateBindingFreeNonMany(nonManyProp, bindedVariable));
                        variablesBindedSoFar.Add(bindedVariable);
                    }
                }

                // We compute the checks that we can do right now, and the ones that must be post poned because their variables are not binded yet
                // For now we only do checks on single value properties, the many valued one are simply exhaustively explored/binded later
                IEnumerable<IPropertyTemplateItem> candidatesInit = current.Part.Where(prop => !prop.ReferredProperty.isMany() && (prop.Value is CSharpOpaqueExpression || prop.Value is IVariableExp));
                ISet<IPropertyTemplateItem> candidates = new HashSet<IPropertyTemplateItem>();
                candidates.UnionWith(candidatesInit);
                candidates.UnionWith(postPonedPropertiesToCheck);
                candidates.ExceptWith(managedProps);
                ISet<IPropertyTemplateItem> propsToCheck = new HashSet<IPropertyTemplateItem>();
                foreach (IPropertyTemplateItem candidate in candidates)
                {
                    if (!variablesBindedSoFar.IsSupersetOf(QvtModelExplorer.FindBindedVariables(candidate)))
                    {
                        propsToCheck.Remove(candidate);
                        postPonedPropertiesToCheck.Add(candidate);
                    }
                    else
                    {
                        propsToCheck.Add(candidate);
                        postPonedPropertiesToCheck.Remove(candidate);
                    }
                }

                // We generate the checks for all the ones that can be made now
                if (propsToCheck.Count > 0)
                {
                    IEnumerable<string> conditions = propsToCheck.Select(u => GenerateConditionnalProperty(u, true));
                    string condition = string.Join(" && ", conditions);
                    stringBuilder.AppendLine("if (" + condition + ") {");
                }

                // We make a recursion for each object template not managed yet
                // - If the ref is many, then we make the binding first using a loop
                // - If the ref is not many, the binding was done before when managing non-many

                List<IPropertyTemplateItem> objectTemplatesManyRemaining = current.Part.Where(p => p.Value is ObjectTemplateExp && p.ReferredProperty.isMany() && remaining.Contains(p.Value)).ToList();
                foreach (IPropertyTemplateItem propWithTemplate in objectTemplatesManyRemaining)
                {
                    // Generate start for each, which binds the variable associated with the object template
                    ObjectTemplateExp objectTemplate = (ObjectTemplateExp)propWithTemplate.Value;
                    stringBuilder.AppendLine("foreach (" + objectTemplate.BindsTo.Type.GetRealTypeName() + " " + objectTemplate.BindsTo.Name + "  in " + currentVariableName + "." + propWithTemplate.ReferredProperty.Name + ".OfType<" + propWithTemplate.ReferredProperty.Type.GetRealTypeName() + ">()) {");
                    variablesBindedSoFar.Add(objectTemplate.BindsTo);
                }

                GenerateDomainCheckMethodContent(sourceDomain, variablesBindedSoFar, analysis, remaining, stringBuilder, postPonedPropertiesToCheck);

                foreach (IPropertyTemplateItem _ in objectTemplatesManyRemaining)
                {
                    // Generate end for each
                    stringBuilder.AppendLine("}");
                }

                // Generate end if checks all c# expressions
                if (propsToCheck.Count > 0)
                {
                    stringBuilder.Append("}");
                }

                // End conditional on the object template
                stringBuilder.AppendLine("}");
            }

            // We stop the recursion if there are no more object templates to manage
            else
            {
                string matchClassName = QvtCodeGeneratorStrings.MatchDomainClassName(sourceDomain);

                // Now we can finally create the Match object
                stringBuilder.AppendLine(matchClassName + " match = new " + matchClassName + "() {");

                foreach (IVariable variable in analysis.VariablesItCanBind)
                {
                    stringBuilder.AppendLine(variable.Name + " = " + variable.Name + ",");
                }

                stringBuilder.AppendLine("};");
                stringBuilder.AppendLine("result.Add(match);");
            }

            return stringBuilder.ToString();
        }

        public static string GenerateExtractVariablesFromMatch(DomainVariablesBindingsResult domainAnalysisResult, ISet<IVariable> variablesAlreadyBinded, string bindingsContainer, bool onlyNonPrimitive = false)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (IVariable variable in domainAnalysisResult.VariablesItCanBind.Where(v => !variablesAlreadyBinded.Contains(v) && (!onlyNonPrimitive || !(v.Type is IPrimitiveType))))
            {
                stringBuilder.AppendLine(variable.Type.GetRealTypeName() + " " + variable.Name + " = " + bindingsContainer + "." + variable.Name + ";");
                variablesAlreadyBinded.Add(variable);
            }
            return stringBuilder.ToString();
        }

        private static IList<IList<IPropertyTemplateItem>> FindKeyApplicablePropertyPaths(IObjectTemplateExp objectTemplateExpression, IKey candidateKey)
        {
            // First, we try to find if some keys applies here
            IList<IList<IPropertyTemplateItem>> result = new List<IList<IPropertyTemplateItem>>();

            IList<IList<IPropertyTemplateItem>> candidateKeyPropertyPaths = new List<IList<IPropertyTemplateItem>>();

            bool isApplicable = true;
            foreach (IProperty property in candidateKey.Part)
            {
                IPropertyTemplateItem candidate = objectTemplateExpression.Part.FirstOrDefault(p => p.ReferredProperty == property);
                if (candidate != null)
                {
                    List<IPropertyTemplateItem> propertyPath = new List<IPropertyTemplateItem>() { candidate };
                    candidateKeyPropertyPaths.Add(propertyPath);
                }
                else
                {
                    isApplicable = false;
                    break;
                }
            }

            if (isApplicable)
            {
                foreach (PropertyPath propertyPath in candidateKey.PropertyPaths())
                {
                    IObjectTemplateExp currentobjectTemplateExpression = objectTemplateExpression;
                    bool isPathApplicable = true;
                    List<IPropertyTemplateItem> concretePropertyPath = new List<IPropertyTemplateItem>();
                    foreach (IProperty property in propertyPath.Properties)
                    {
                        IEnumerable<IPropertyTemplateItem> nextObjectTemplate = currentobjectTemplateExpression.Part.Where(p => p.ReferredProperty == property).ToList();
                        if (!nextObjectTemplate.IsNullOrEmpty())
                        {
                            concretePropertyPath.Add(nextObjectTemplate.Single());
                            currentobjectTemplateExpression = (IObjectTemplateExp)nextObjectTemplate.Single().Value;
                        }
                        else
                        {
                            isPathApplicable = false;
                            break;
                        }
                    }
                    if (isPathApplicable)
                    {
                        candidateKeyPropertyPaths.Add(concretePropertyPath);
                    }
                    else
                    {
                        isApplicable = false;
                        break;
                    }
                }
            }
            // If this key is valid, we merge all its contents into the result
            if (isApplicable)
            {
                foreach (IList<IPropertyTemplateItem> candidateKeyPropertyPath in candidateKeyPropertyPaths)
                {
                    IList<IPropertyTemplateItem> existing = result.FirstOrDefault(pp => AreEqual(pp, candidateKeyPropertyPath));
                    if (existing.IsNullOrEmpty())
                    {
                        result.Add(candidateKeyPropertyPath);
                    }
                }
                return result;
            }
            return null;
        }

        public static bool AreEqual(IList<IPropertyTemplateItem> pp1, IList<IPropertyTemplateItem> pp2)
        {
            foreach (IPropertyTemplateItem pp1Item in pp1)
            {
                IPropertyTemplateItem pp2Item = pp2[pp1.IndexOf(pp1Item)];
                if (pp1Item != pp2Item)
                    return false;
            }
            return true;
        }

        private static readonly ISet<string> cSharpPrimitiveTypes = new HashSet<string>()
        {
            "int", "bool", "double", "byte", "sbyte", "Int16", "UInt16", "Int32", "UInt32", "Int64", "UInt64", "IntPtr", "UIntPtr", "char", "Single"
        };

        private static string GenerateAccess(string varname, IList<IPropertyTemplateItem> applicablePropertyPath)
        {
            string access = varname;
            foreach (IPropertyTemplateItem property in applicablePropertyPath)
            {
                access += "?." + property.ReferredProperty.Name;
            }
            IPropertyTemplateItem last = applicablePropertyPath.Last();
            if (last.ReferredProperty.Type is IDataType)
            {
                if (cSharpPrimitiveTypes.Contains(last.ReferredProperty.Type.GetRealTypeName()))
                {
                    access += "GetValueOrDefault()";
                }
            }
            return access;
        }

        private static string GenerateFindValueWithKey(IPropertyTemplateItem propertyTemplateItem, IObjectTemplateExp objectTemplateExpression, string resultContainer, IList<IList<IPropertyTemplateItem>> applicablePropertyPaths, bool useMetamodelInterface)
        {
            StringBuilder sb = new StringBuilder();
            IList<string> conditions = new List<string>();
            string variableName = "var" + new Random().Next();
            foreach (IList<IPropertyTemplateItem> applicablePropertyPath in applicablePropertyPaths)
            {
                string access = GenerateAccess(variableName, applicablePropertyPath);
                string condition = access + " == " + GenerateExpression(applicablePropertyPath.Last().Value, useMetamodelInterface);
                conditions.Add(condition);
            }
            string completeCondition = string.Join(" && ", conditions);

            // If the candidate is in a collection, we generate a query to find it
            if (propertyTemplateItem.ReferredProperty.isMany())
            {
                sb.AppendLine(objectTemplateExpression.BindsTo.Name + " = "
                              + resultContainer + "." + propertyTemplateItem.ReferredProperty.Name
                              + ".OfType<" + propertyTemplateItem.ReferredProperty.Type.GetRealTypeName() + ">()"
                              + ".FirstOrDefault(" + variableName + " => " + completeCondition + ");");
            }

            // If the candidate is in a ref, we generate a conditional to find it
            else
            {
                sb.AppendLine("{"
                              + propertyTemplateItem.ReferredProperty.Type.GetRealTypeName()
                              + variableName + "= " + resultContainer + "." + propertyTemplateItem.ReferredProperty.Name + ";"
                              + "if (" + completeCondition + ") {"
                              + objectTemplateExpression.BindsTo.Name + " = "
                              + variableName + ";"
                              + "}"
                              + "}");
            }
            return sb.ToString();
        }

        private static string GenerateCheckAndAddDictionnary(IList<IList<IPropertyTemplateItem>> propertyPathsTransformationKey, string varName, IKey transformationKey)
        {
            StringBuilder sb = new StringBuilder();
            IList<string> tupleTypes = propertyPathsTransformationKey.Select(pp => pp.Last().ReferredProperty.Type.GetRealTypeName()).Select(name => name + (cSharpPrimitiveTypes.Contains(name) ? "?" : "")).ToList();
            IList<string> tupleValues = propertyPathsTransformationKey.Select(pp => GenerateAccess(varName, pp)).ToList();

            string tupleTypesCombined = string.Join(",", tupleTypes);
            string tupleValuesCombined = string.Join(",", tupleValues);

            string testDictionnary = "transformation."
                                     + QvtCodeGeneratorStrings.KeyDictionnaryName(transformationKey)
                                     + ".ContainsKey(new Tuple<" + tupleTypesCombined + ">(" + tupleValuesCombined + "))";
            string accessDictionnaryWrite = "transformation."
                                            + QvtCodeGeneratorStrings.KeyDictionnaryName(transformationKey)
                                            + "[new Tuple<" + tupleTypesCombined + ">(" + tupleValuesCombined + ")]";
            sb.AppendLine("if (" + testDictionnary + ") {");
            sb.AppendLine("throw new Exception(\"Two objects cannot have the same key\");");
            sb.AppendLine("} else {");
            sb.AppendLine(accessDictionnaryWrite + "=" + varName + ";");
            sb.AppendLine("}");
            return sb.ToString();
        }

        private static string GenerateSetValue(IPropertyTemplateItem propertyTemplateItem, string resultContainer, IRelation relation, bool useMetamodelInterface)
        {
            StringBuilder sb = new StringBuilder();
            IObjectTemplateExp objectTemplateExpression = propertyTemplateItem.Value as IObjectTemplateExp;

            // Case ref value
            if (objectTemplateExpression != null)
            {
                IKey transformationKey = ((IRelationalTransformation)relation.Transformation).OwnedKey.FirstOrDefault(k => k.Identifies == objectTemplateExpression.ReferredClass);
                IList<IList<IPropertyTemplateItem>> propertyPathsTransformationKey = transformationKey != null ? FindKeyApplicablePropertyPaths(objectTemplateExpression, transformationKey) : null;
                IKey relationKey = relation.Keys().FirstOrDefault(k => k.Identifies == objectTemplateExpression.ReferredClass);
                IList<IList<IPropertyTemplateItem>> propertyPathsRelationKey = relationKey != null ? FindKeyApplicablePropertyPaths(objectTemplateExpression, relationKey) : null;

                // We always start by creating the empty variable that will contain the object
                sb.AppendLine(objectTemplateExpression.BindsTo.Type.GetRealTypeName() + " " + objectTemplateExpression.BindsTo.Name + " = null;");

                int nbBracketsOpened = 0;

                // If any part of a key applies, then we must generate a test to find a potential existing object
                if (propertyPathsTransformationKey != null || propertyPathsRelationKey != null)
                {
                    // We try to resolve globally and locally, if a global key applies here
                    if (propertyPathsTransformationKey != null)
                    {
                        sb.AppendLine();
                        sb.AppendLine("// Trying to resolve the object\'" + objectTemplateExpression.BindsTo.Name + "\' globally using the transformation key");
                        IList<string> tupleTypes = propertyPathsTransformationKey.Select(pp => pp.Last().ReferredProperty.Type.GetRealTypeName()).Select(name => name + (cSharpPrimitiveTypes.Contains(name) ? "?" : "")).ToList();
                        IList<string> tupleValues = propertyPathsTransformationKey.Select(pp => GenerateAccess(objectTemplateExpression.BindsTo.Name, pp)).ToList();
                        IList<string> tupleValuesRead = propertyPathsTransformationKey.Select(pp => GenerateExpression(pp.Last().Value, useMetamodelInterface)).ToList();

                        string tupleTypesCombined = string.Join(",", tupleTypes);
                        string tupleValuesCombined = string.Join(",", tupleValues);
                        string tupleValuesCombinedRead = string.Join(",", tupleValuesRead);
                        string accessDictionnaryReadSafe = "transformation."
                                                           + QvtCodeGeneratorStrings.KeyDictionnaryName(transformationKey)
                                                           + ".TryGetValue(new Tuple<" + tupleTypesCombined + ">(" + tupleValuesCombinedRead + "), out " + objectTemplateExpression.BindsTo.Name + ")";
                        string accessDictionnaryWrite = "transformation."
                                                        + QvtCodeGeneratorStrings.KeyDictionnaryName(transformationKey)
                                                        + "[new Tuple<" + tupleTypesCombined + ">(" + tupleValuesCombined + ")]";
                        sb.AppendLine(accessDictionnaryReadSafe + ";");

                        // If not found globally with global key, we try to find locally with global key
                        sb.AppendLine("// If the object wasn't found globally, we try to find it locally");
                        sb.AppendLine("if (" + objectTemplateExpression.BindsTo.Name + "== null) {");
                        sb.AppendLine(GenerateFindValueWithKey(propertyTemplateItem, objectTemplateExpression, resultContainer, propertyPathsTransformationKey, useMetamodelInterface));
                        nbBracketsOpened++;

                        // If we found locally with global key, we add to the dictionnary
                        sb.AppendLine("// If the object was found locally, we add it to the global cache");
                        sb.AppendLine("if (" + objectTemplateExpression.BindsTo.Name + "!= null) {");
                        sb.AppendLine(accessDictionnaryWrite + " = " + objectTemplateExpression.BindsTo.Name + ";");

                        // Else we continue with other attempts (local key or object creation)
                        sb.AppendLine("}");
                    }

                    if (propertyPathsRelationKey != null)
                    {
                        sb.AppendLine("// Trying to resolve the object locally using the relation key");

                        if (propertyPathsTransformationKey != null)
                        {
                            sb.AppendLine("else {");
                            nbBracketsOpened++;
                        }

                        sb.AppendLine(GenerateFindValueWithKey(propertyTemplateItem, objectTemplateExpression, resultContainer, propertyPathsRelationKey, useMetamodelInterface));

                        // If we found something locally, once again we check in the dictionnary and try to update 
                        sb.AppendLine("// If the object was found locally, we add it to the global cache");
                        sb.AppendLine("if (" + objectTemplateExpression.BindsTo.Name + "!= null) {");
                        sb.AppendLine(GenerateCheckAndAddDictionnary(propertyPathsTransformationKey, objectTemplateExpression.BindsTo.Name, transformationKey));
                        sb.AppendLine("}");
                    }

                    // In the end we generate a test if we found a candidate
                    sb.AppendLine("// If the object still doesn't exist, we create it");
                    sb.AppendLine("else {");
                    nbBracketsOpened ++;
                }

                // In any case, we generate the code to create the object, even if it might end in the conditional due to the keys test
                string beginningCreation = objectTemplateExpression.BindsTo.Name + " = ";
                if (useMetamodelInterface)
                    sb.AppendLine(beginningCreation + " (" + objectTemplateExpression.BindsTo.Type.GetRealTypeName() + ") editor." + nameof(IMetaModelInterface.CreateNewObjectInField) + "(" + resultContainer + ", \"" + propertyTemplateItem.ReferredProperty.Name + "\");");
                else
                {
                    string res = beginningCreation;
                    res += "new " + objectTemplateExpression.BindsTo.Type.GetRealTypeName() + "();\n";
                    if (!propertyTemplateItem.ReferredProperty.isMany())
                        res += resultContainer + "." + propertyTemplateItem.ReferredProperty.Name + " = " + objectTemplateExpression.BindsTo.Name + ";";
                    else
                        res += resultContainer + "." + propertyTemplateItem.ReferredProperty.Name + ".Add(" + objectTemplateExpression.BindsTo.Name + ");";
                    sb.AppendLine(res);
                }

                if (propertyPathsTransformationKey != null)
                {
                    // TODO generate this all the time?
                    sb.AppendLine("// We add the created object to the global cache");
                    sb.AppendLine(GenerateCheckAndAddDictionnary(propertyPathsTransformationKey, objectTemplateExpression.BindsTo.Name, transformationKey));
                }
                // If we generated a conditional for the keys test, we close it now
                if (propertyPathsTransformationKey != null || propertyPathsRelationKey != null)
                    sb.AppendLine("}");

                for (int i = 0; i < nbBracketsOpened - 1; i++)
                {
                    sb.AppendLine("}");
                }
                return sb.ToString();
            }
            // Case primitive value (variable use or c sharp expression)
            else
            {
                if (useMetamodelInterface)
                    return ("editor." + nameof(IMetaModelInterface.AddOrSetInField) + "(" + resultContainer + ", \"" + propertyTemplateItem.ReferredProperty.Name + "\", " + GenerateExpression(propertyTemplateItem.Value, true) + " );");
                else
                    return resultContainer + "." + propertyTemplateItem.ReferredProperty.Name + "=" + GenerateExpression(propertyTemplateItem.Value, false) + ";";
            }
        }

        public static string GenerateConstructTarget(IRelationDomain targetDomain, bool useMetamodelInterface)
        {
            StringBuilder stringBuilder = new StringBuilder();
            ISet<string> postPonedSets = new HashSet<string>();

            List<IObjectTemplateExp> objectTemplates = QvtModelExplorer.FindAllObjectTemplates(targetDomain).Where(o => !o.IsAntiTemplate()).ToList();
            foreach (IObjectTemplateExp objectTemplate in objectTemplates)
            {
                IVariable variable = objectTemplate.BindsTo;
                stringBuilder.AppendLine("\n// Contructing " + variable.Name);

                foreach (IPropertyTemplateItem propertyTemplateItem in objectTemplate.Part)
                {
                    string setStatement = GenerateSetValue(propertyTemplateItem, variable.Name, (IRelation)targetDomain.Rule, useMetamodelInterface);
                    IVariableExp targetVariableValue = propertyTemplateItem.Value as IVariableExp;
                    IObjectTemplateExp targetObjTemplateValue = objectTemplates.FirstOrDefault(o => o.BindsTo == targetVariableValue?.ReferredVariable);
                    bool ok = targetObjTemplateValue == null || objectTemplates.IndexOf(targetObjTemplateValue) < objectTemplates.IndexOf(objectTemplate);

                    if (ok)
                    {
                        stringBuilder.AppendLine(setStatement);
                    }
                    else
                    {
                        postPonedSets.Add(setStatement);
                    }
                }
            }
            if (!postPonedSets.IsNullOrEmpty())
            {
                stringBuilder.AppendLine("// Setting cycling properties");
                foreach (string setStatement in postPonedSets)
                {
                    stringBuilder.AppendLine(setStatement);
                }
            }
            return stringBuilder.ToString();
        }

        private static string GenerateConditionnalProperty(IPropertyTemplateItem prop, bool useMetamodelInterface)
        {
            string right = GenerateExpression(prop.Value, useMetamodelInterface);
            return prop.ObjContainer.BindsTo.Name + "." + prop.ReferredProperty.Name + " == " + right;
        }

        public static string GenerateExpression(IOclExpression expression, bool useMetamodelInterface)
        {
            if (expression is IOperationCallExp)
            {
                IOperationCallExp value = (IOperationCallExp)expression;
                return "transformation.Functions." + value.ReferredOperation.Name + "(" + string.Join(",", value.Argument.Select(u => GenerateExpression(u, useMetamodelInterface))) + ")";
            }
            if (expression is CSharpOpaqueExpression)
            {
                CSharpOpaqueExpression value = (CSharpOpaqueExpression)expression;
                return value.Code;
            }
            if (expression is IVariableExp)
            {
                return ((IVariableExp)expression).ReferredVariable.Name;
            }
            if (expression is Assignment)
            {
                Assignment assignment = (Assignment)expression;
                return assignment.AssignedVariable.Type.GetRealTypeName() + " " + assignment.AssignedVariable.Name + " = " + GenerateExpression(assignment.Value, useMetamodelInterface);
            }
            if (expression is IRelationCallExp)
            {
                IRelationCallExp relationCallExp = (IRelationCallExp)expression;
                return "transformation." + QvtCodeGeneratorStrings.RelationClassName(relationCallExp.ReferredRelation)
                       + ".CheckAndEnforce(" + string.Join(",", relationCallExp.Argument.Select(u => GenerateExpression(u, useMetamodelInterface))) + ")";
            }
            throw new CodeGeneratorException("Cannot manage expression: " + expression);
        }

        public static string GenerateAssignmentsFromRelationCall(IRelationCallExp relationCallExp, bool useMetamodelInterface)
        {
            return "transformation." + QvtCodeGeneratorStrings.RelationClassName(relationCallExp.ReferredRelation)
                   + ".FindPreviousResult(" + string.Join(",", relationCallExp.Argument.Take(relationCallExp.ReferredRelation.Domain.Count(d => !d.IsEnforceable.GetValueOrDefault())).Select(u => GenerateExpression(u, useMetamodelInterface))) + ")";
        }
    }
}