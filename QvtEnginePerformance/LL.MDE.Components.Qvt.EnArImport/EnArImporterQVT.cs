using System;
using System.Collections.Generic;
using System.Linq;

using LL.MDE.Components.Common.EnArLoader;
using LL.MDE.Components.Qvt.EnArImport.Util;
using LL.MDE.Components.Qvt.Metamodel.CustomExtensions.EMOFExtensions;
using LL.MDE.Components.Qvt.Metamodel.CustomExtensions.QVTRelationExtensions;
using LL.MDE.Components.Qvt.Metamodel.CustomExtensions.QVTTemplateExtensions;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using NMF.Utilities;

using EMOF = LL.MDE.Components.Qvt.Metamodel.EMOF;
using QVTBase = LL.MDE.Components.Qvt.Metamodel.QVTBase;
using QVTRelations = LL.MDE.Components.Qvt.Metamodel.QVTRelation;
using QVTTemplate = LL.MDE.Components.Qvt.Metamodel.QVTTemplate;
using EssentialOCL = LL.MDE.Components.Qvt.Metamodel.EssentialOCL;
using EnAr = LL.MDE.DataModels.EnAr;

namespace LL.MDE.Components.Qvt.EnArImport
{
    public class EnArImporterQVT
    {
        private readonly EnArExplorer explorer;
        private readonly EnArImporterEMOF emofImporter;
        private readonly List<EMOF.IPackage> metamodels = new List<EMOF.IPackage>();
        private readonly Dictionary<string, EMOF.IPackage> aliases = new Dictionary<string, EMOF.IPackage>();
        private readonly Dictionary<EnAr.Element, QVTTemplate.IObjectTemplateExp> objectElementToObjectTemplate = new Dictionary<EnAr.Element, QVTTemplate.IObjectTemplateExp>();
        private readonly ISet<QVTRelations.IRelation> relationsWithKeys = new HashSet<QVTRelations.IRelation>();

        public EnArImporterQVT(EnArExplorer explorer)
        {
            this.explorer = explorer;
            this.emofImporter = new EnArImporterEMOF(explorer);
            Tuple<List<EMOF.IPackage>, Dictionary<string, EMOF.IPackage>> res = this.emofImporter.ConstructMetamodels();
            metamodels.AddRange(res.Item1);
            foreach (KeyValuePair<string, EMOF.IPackage> keyValuePair in res.Item2)
            {
                aliases[keyValuePair.Key] = keyValuePair.Value;
            }
        }

        private QVTTemplate.IPropertyTemplateItem ConstructPropertyTemplateItem(QVTRelations.IRelation relation, QVTRelations.IDomainPattern domainPattern, QVTTemplate.IObjectTemplateExp objectTemplateExp, RunStateField runStateField)
        {
            ISet<EMOF.IProperty> atts = objectTemplateExp.ReferredClass.GetAllInheritedAttributes();
            EMOF.IProperty property = atts.Single(p => string.Equals(p.Name, runStateField.Variable, StringComparison.CurrentCultureIgnoreCase));
            QVTTemplate.IPropertyTemplateItem propertyTemplateItem = null;
            if (property != null)
            {
                EssentialOCL.IOclExpression expression = ConstructOCLExpression(relation, runStateField.Value, domainPattern, property.Type);

                propertyTemplateItem = new QVTTemplate.PropertyTemplateItem()
                {
                    ReferredProperty = property,
                    Value = expression,
                    IsOpposite = false, // TODO?
                    ObjContainer = objectTemplateExp
                };
            }

            return propertyTemplateItem;
        }

        private QVTTemplate.IPropertyTemplateItem ConstructPropertyTemplateItem(QVTRelations.IRelation relation, QVTRelations.IDomainPattern domainPattern, QVTTemplate.IObjectTemplateExp objectTemplateExp, EnAr.Connector connector, EnAr.ConnectorEnd connectorEnd, EnAr.Element linkedElement, ISet<EnAr.Connector> visitedConnectors)
        {
            EssentialOCL.IOclExpression value;
            EMOF.IType type;
            QVTTemplate.IObjectTemplateExp targetObjectTemplateExp;

            ISet<EnAr.Connector> realVisitedConnectors = visitedConnectors ?? new HashSet<EnAr.Connector>();
            ISet<EnAr.Connector> realVisitedConnectorsPropagated = new HashSet<EnAr.Connector>(realVisitedConnectors);
            realVisitedConnectorsPropagated.Add(connector);

            // Case cycle among object templates: simple variable expression
            if (objectElementToObjectTemplate.ContainsKey(linkedElement))
            {
                targetObjectTemplateExp = objectElementToObjectTemplate[linkedElement];
                EssentialOCL.IVariable existingVariable = targetObjectTemplateExp.BindsTo;
                value = new EssentialOCL.VariableExp()
                {
                    ReferredVariable = existingVariable
                };
                type = existingVariable.Type;
            }
            // Case no cycle; recursive creation of object template
            else
            {
                targetObjectTemplateExp = ConstructObjectTemplateExp(relation, domainPattern, linkedElement, realVisitedConnectorsPropagated);
                value = targetObjectTemplateExp;
                type = ((QVTTemplate.IObjectTemplateExp)value).BindsTo.Type;
            }

            EMOF.IProperty property = null;

            // If the connector end has a role, we use it to find the corresponding EMOF property
            if (!string.IsNullOrWhiteSpace(connectorEnd.Role))
            {
                property = objectTemplateExp.ReferredClass.GetAllInheritedAttributes().Single(p => p.Name == connectorEnd.Role);
            }

            // If the connector end has no role (due to the else)
            // AND if we haven't visited the connector yet 
            // AND if the connector has no roles whatsoever, we try to guess the type
            else if (!realVisitedConnectors.Contains(connector) && connector.ClientEnd.Role.IsNullOrEmpty() && connector.SupplierEnd.Role.IsNullOrEmpty())
            {
                IList<EMOF.IProperty> candidateProperties = objectTemplateExp.ReferredClass.GetAllInheritedAttributes().Where(p =>
                    (p.Type as EMOF.IClass)?.GetAllSubTypes().Contains(type) ?? p.Type == type).ToList();

                if (candidateProperties.Count == 0)
                {
                    throw new InvalidQVTRelationsModelException("Relation " + relation.Name
                                                                + ", invalid property connector between " + objectTemplateExp.BindsTo.Name + " and " + targetObjectTemplateExp.BindsTo.Name
                                                                + ", no possible property", connector);
                }
                if (candidateProperties.Count > 1)
                {
                    throw new InvalidQVTRelationsModelException("Relation " + relation.Name
                                                                + ", ambiguous property connector between " + objectTemplateExp.BindsTo.Name + " and " + targetObjectTemplateExp.BindsTo.Name
                                                                + ", cannot choose a property between the following: [" + string.Join(";", candidateProperties.Select(p => p.Name)) + "]", connector);
                }
                property = candidateProperties.Single();
            }

            QVTTemplate.PropertyTemplateItem propertyTemplateItem = null;
            if (property != null)
            {
                propertyTemplateItem = new QVTTemplate.PropertyTemplateItem()
                {
                    ReferredProperty = property,
                    Value = value,
                    IsOpposite = false, // TODO?
                    ObjContainer = objectTemplateExp
                };
            }

            return propertyTemplateItem;
        }

        private EssentialOCL.IVariable ConstructVariable(QVTRelations.IRelation relation, string name, EMOF.IType type = null)
        {
            EssentialOCL.IVariable variable = relation.Variable.FirstOrDefault(v => v.Name == name);
            if (variable == null)
            {
                variable = new EssentialOCL.Variable()
                {
                    Name = name,
                    Type = type,
                    InitExpression = null, // Not taken into account
                };
                relation.Variable.Add(variable);
            }
            // If there was an existing variable, but with no type yet, we give it the input type
            else if (variable.Type == null && type != null)
            {
                variable.Type = type;
            }
            return variable;
        }

        private EssentialOCL.IVariable ConstructVariable(QVTRelations.IRelation relation, EnAr.Element objectElement)
        {
            return ConstructVariable(relation, objectElement.Name, emofImporter.ConstructTypeOfTyped(objectElement));
        }

        private QVTTemplate.IObjectTemplateExp ConstructObjectTemplateExp(QVTRelations.IRelation relation, QVTRelations.IDomainPattern domainPattern, EnAr.Element objectElement, ISet<EnAr.Connector> visitedConnectors = null)
        {
            EssentialOCL.IVariable variable = null;
            EMOF.IType type = emofImporter.ConstructTypeOfTyped(objectElement);

            // TODO manage the not ?
            if (objectElement.Name != "{not}")
            {
                variable = ConstructVariable(relation, objectElement);
                domainPattern?.BindsTo.Add(variable);
            }

            QVTTemplate.IObjectTemplateExp objectTemplateExp = new QVTTemplate.ObjectTemplateExp()
            {
                BindsTo = variable,
                ReferredClass = type as EMOF.IClass,
                Where = null // TODO
            };
            objectTemplateExp.SetAntiTemplate(variable == null);
            objectElementToObjectTemplate.Add(objectElement, objectTemplateExp);

            foreach (RunStateField runStateField in EnArExplorer.GetRunState(objectElement))
            {
                ConstructPropertyTemplateItem(relation, domainPattern, objectTemplateExp, runStateField);
            }

            foreach (EnAr.Connector connector in explorer.GetConnectorsLinkedTo(objectElement).FindAll(c => c.Stereotype != "qvtTransformationLink"))
            {
                Tuple<EnAr.ConnectorEnd, EnAr.Element> linked = explorer.GetElementOppositeTo(objectElement, connector);
                EnAr.ConnectorEnd connectorEnd = linked.Item1;
                EnAr.Element linkedElement = linked.Item2;
                //if (!string.IsNullOrWhiteSpace(connectorEnd.Role))
                //{
                ConstructPropertyTemplateItem(relation, domainPattern, objectTemplateExp, connector, connectorEnd, linkedElement, visitedConnectors);
                //}
            }

            return objectTemplateExp;
        }

        private QVTRelations.IDomainPattern ConstructDomainPattern(QVTRelations.IRelation relation, EnAr.Element domainObjectElement)
        {
            QVTRelations.IDomainPattern domainPattern = new QVTRelations.DomainPattern()
            {
                // Predicate = null, // TODO
            };

            QVTTemplate.IObjectTemplateExp objectTemplateExp = ConstructObjectTemplateExp(relation, domainPattern, domainObjectElement);
            domainPattern.TemplateExpression = objectTemplateExp;

            return domainPattern;
        }

        private QVTBase.ITypedModel ConstructTypedModel(QVTRelations.IRelationalTransformation relationTransformation, EnAr.Connector qvtTransformationLinkConnector)
        {
            // We determine the typedmodel based on the FQN given on the connector
            string modelNameTag = explorer.GetTaggedValue(qvtTransformationLinkConnector, "modelName");
            string metaModelNameTag = explorer.GetTaggedValue(qvtTransformationLinkConnector, "metaModelName");
            string typedModelName = "";
            string metamodelFQNOrAlias = "";
            EMOF.IPackage metamodelPackage = null;
            if (!modelNameTag.IsNullOrEmpty())
            {
                if (modelNameTag.Contains(':'))
                {
                    string[] split = modelNameTag.Split(':');
                    typedModelName = split[0];
                    metamodelFQNOrAlias = split[1];
                }
                else if (metaModelNameTag != null)
                {
                    typedModelName = modelNameTag;
                }
            }

            if (metamodelFQNOrAlias.IsNullOrEmpty() && metaModelNameTag != null)
            {
                // Case real link
                if (metaModelNameTag.StartsWith("{"))
                {
                    EnAr.Package enArMetamodelPackage = explorer.GetPackageByGuid(metaModelNameTag);
                    metamodelPackage = emofImporter.GetEMOFPackage(enArMetamodelPackage);
                }
                // Case string name
                else
                {
                    metamodelFQNOrAlias = metaModelNameTag;
                }
            }

            if (metamodelPackage == null)
            {
                // The metamodel package can be found either using the FQN or one of its aliases
                metamodelPackage = metamodels.FirstOrDefault(metamodel => EnArImporterEMOF.GetFQN(metamodel) == metamodelFQNOrAlias || (aliases.ContainsKey(metamodelFQNOrAlias) && metamodel == aliases[metamodelFQNOrAlias]));
            }

            /*if (typedModelName.IsNullOrEmpty() && metamodelPackage == null)
            {
                throw new InvalidQVTRelationsModelException("A domain link must either indicate the model name with the pattern <model name>:<metamodel name>, or must provide a tag 'metaModelName'.", qvtTransformationLinkConnector);
            }*/

            // Case primitive domains... could probably be managed better
            if (metamodelPackage == null)
            {
                return null;
            }

            // We first check that the relational transformation doesn't already have this typed model
            QVTBase.ITypedModel typedModel = relationTransformation.ModelParameter.FirstOrDefault(p => (typedModelName.IsNullOrEmpty() || p.Name == typedModelName) && p.UsedPackage.FirstOrDefault(p2 => p2 == metamodelPackage) != null);

            // If there is none, we create one
            if (typedModel == null)
            {
                typedModel = new QVTBase.TypedModel()
                {
                    Name = typedModelName,
                    Transformation = relationTransformation,
                };
                typedModel.UsedPackage.Add(metamodelPackage);
            }

            return typedModel;
        }

        private QVTRelations.IRelationDomain ConstructNonPrimitiveRelationDomain(QVTRelations.IRelation relation, QVTBase.ITypedModel typedModel, EnAr.Connector qvtTransformationLinkConnector, EnAr.Element domainObjectElement)
        {
            //if (domainObjectElement.Stereotype != "domain")
            //    throw new InvalidQVTRelationsModelException("A domain element must have the \"domain\" stereotype", domainObjectElement);
            // TODO replace by warning?

            QVTRelations.RelationDomain relationDomain = new QVTRelations.RelationDomain()
            {
                Name = qvtTransformationLinkConnector.Name,
                IsCheckable = true,
                IsEnforceable = explorer.GetTaggedValue(qvtTransformationLinkConnector, "CEType").ToLower() == "enforce",
                TypedModel = typedModel,
                //DefaultAssignment = null // TODO
                Rule = relation
            };

            QVTRelations.IDomainPattern domainpattern = ConstructDomainPattern(relation, domainObjectElement);
            relationDomain.Pattern = domainpattern;
            relationDomain.RootVariable = domainpattern.TemplateExpression.BindsTo;
            domainpattern.RelationDomain = relationDomain;
            return relationDomain;
        }

        private QVTRelations.IRelationDomain ConstructPrimitiveRelationDomain(QVTRelations.IRelation relation, EnAr.Connector qvtTransformationLinkConnector, EnAr.Element domainObjectElement)
        {
            EssentialOCL.IVariable variable = ConstructVariable(relation, domainObjectElement);

            QVTRelations.IRelationDomain relationDomain = new QVTRelations.RelationDomain
            {
                Name = qvtTransformationLinkConnector.Name, // a primitive domain  is supposed not to have a name, but we use it for ordering
                IsCheckable = true,
                IsEnforceable = false,
                //DefaultAssignment = null // TODO?
                Rule = relation,
                RootVariable = variable
            };
            relation.Variable.Add(variable);

            return relationDomain;
        }

        private void ConstructRelationDomain(QVTRelations.IRelationalTransformation relationTransformation, QVTRelations.IRelation relation, EnAr.Connector qvtTransformationLinkConnector)
        {
            // We look in the EA "domain" Element pointed by the qvtTransformationLinkConnector
            EnAr.Element domainObjectElement = explorer.GetTargetElement(qvtTransformationLinkConnector);

            // We construct (or get) the typed model, if any
            QVTBase.ITypedModel candidateTypedModel = ConstructTypedModel(relationTransformation, qvtTransformationLinkConnector);

            // If no typed model, it must be a primitive type
            // TODO maybe check for a tag?
            if (candidateTypedModel == null)
            {
                ConstructPrimitiveRelationDomain(relation, qvtTransformationLinkConnector, domainObjectElement);
                return;
            }

            // Else, we construct a regular domain
            ConstructNonPrimitiveRelationDomain(relation, candidateTypedModel, qvtTransformationLinkConnector, domainObjectElement);
        }

        /// <summary>
        /// Construct the OCLExpression corresponding to some CSharp expression included in the model.
        /// Try to create pure OCL, fallback to custom CSharpExpression object if not handled.
        /// </summary>
        /// <param name="relation">The relation containing the expression.</param>
        /// <param name="domainPattern">(optional) The domainPattern containing the expression.</param>
        /// <param name="expressionString">The expression string to parse.</param>
        /// <returns></returns>
        private EssentialOCL.IOclExpression ConstructOCLExpression(QVTRelations.IRelation relation, string expressionString, QVTBase.IPattern pattern, EMOF.IType type)
        {
            // To be able to reuse existing transfos: we consider strings with single quotes as well
            string expressionWithOnlyDoubleQuotes = expressionString.Replace("\'", "\"");

            // We parse using Roslyn
            ExpressionSyntax parsedExpression = CSharpParser.ParseExpression(expressionWithOnlyDoubleQuotes);

            // And we use the expression analysis overload variant
            return ConstructOCLExpression(relation, parsedExpression, pattern, type);
        }

        private EssentialOCL.IOclExpression ConstructOCLExpression(QVTRelations.IRelation relation, ExpressionSyntax parsedExpression, QVTBase.IPattern
            pattern, EMOF.IType type = null)
        {
            // Case single identifier => OCL VariableExp
            if (parsedExpression is IdentifierNameSyntax)
            {
                EssentialOCL.IVariable variable = ConstructVariable(relation, parsedExpression.ToString(), type);
                pattern?.BindsTo.Add(variable);
                return new EssentialOCL.VariableExp()
                {
                    ReferredVariable = variable
                };
            }

            // Case method call => QVT RelationCallExp (if the relation exists) of function call (if the function exists)
            if (parsedExpression is InvocationExpressionSyntax)
            {
                InvocationExpressionSyntax invocationExpressionSyntax = (InvocationExpressionSyntax)parsedExpression;

                // We are only interested in direct calls
                if (invocationExpressionSyntax.Expression is IdentifierNameSyntax)
                {
                    IdentifierNameSyntax methodIdentifier = (IdentifierNameSyntax)invocationExpressionSyntax.Expression;
                    ArgumentListSyntax argumentList = invocationExpressionSyntax.ArgumentList;
                    QVTRelations.IRelation calledRelation = FindRelation((QVTRelations.IRelationalTransformation)(relation.Transformation), methodIdentifier.ToString());
                    QVTBase.IFunction calledFunction = FindFunction((QVTRelations.IRelationalTransformation)(relation.Transformation), methodIdentifier.ToString());
                    if (calledRelation != null)
                    {
                        QVTRelations.RelationCallExp call = new QVTRelations.RelationCallExp
                        {
                            ReferredRelation = calledRelation
                        };

                        if (argumentList.Arguments.Count != calledRelation.Domain.Count)
                        {
                            throw new InvalidQVTRelationsModelException("Relation " + relation.Name + ": wrong number of arguments in relation call " + calledRelation.Name);
                        }

                        foreach (ArgumentSyntax argumentSyntax in argumentList.Arguments)
                        {
                            QVTRelations.IRelationDomain correspondingDomain = (QVTRelations.IRelationDomain)calledRelation.Domain[argumentList.Arguments.IndexOf(argumentSyntax)];
                            ExpressionSyntax argumentExpression = argumentSyntax.Expression;
                            EssentialOCL.IOclExpression argumentOCLExpression = ConstructOCLExpression(relation, argumentExpression, pattern, correspondingDomain.RootVariable.Type);
                            call.Argument.Add(argumentOCLExpression);
                        }

                        return call;
                    }
                    else if (calledFunction != null)
                    {
                        string methodname = methodIdentifier.ToString();
                        EssentialOCL.IOperationCallExp call = new EssentialOCL.OperationCallExp()
                        {
                            Type = calledFunction.Type,
                            ReferredOperation = calledFunction,
                            Name = calledFunction.Name,
                        };

                        foreach (ArgumentSyntax argumentSyntax in argumentList.Arguments)
                        {
                            ExpressionSyntax argumentExpression = argumentSyntax.Expression;
                            EssentialOCL.IOclExpression argumentOCLExpression = ConstructOCLExpression(relation, argumentExpression, pattern, calledFunction.Type);
                            call.Argument.Add(argumentOCLExpression);
                        }

                        return call;
                    }
                }
            }

            // Case assignment => Custom Assignment //TODO replace by OCL '=='? Meaning having to provide basic parts of OCL standard lib
            if (parsedExpression is AssignmentExpressionSyntax)
            {
                AssignmentExpressionSyntax assignmentExpressionSyntax = (AssignmentExpressionSyntax)parsedExpression;
                IdentifierNameSyntax leftIdentifier = (IdentifierNameSyntax)assignmentExpressionSyntax.Left;
                ExpressionSyntax right = assignmentExpressionSyntax.Right;
                EssentialOCL.IVariable variable = ConstructVariable(relation, leftIdentifier.ToString());
                pattern?.BindsTo.Add(variable);
                return new EssentialOCL.Assignment()
                {
                    AssignedVariable = variable,
                    Value = ConstructOCLExpression(relation, right, pattern)
                };
            }
            // Any other case => Custom CSharpOpaqueExpression // TODO replace by QVT "Function" with a black box implementation?
            EssentialOCL.CSharpOpaqueExpression cSharpOpaqueExpression = new EssentialOCL.CSharpOpaqueExpression()
            {
                Code = parsedExpression.ToString()
            };
            SetBindings(relation, pattern, cSharpOpaqueExpression, parsedExpression);
            return cSharpOpaqueExpression;
        }

        private QVTBase.IFunction FindFunction(QVTRelations.IRelationalTransformation relationalTransformation, string toString)
        {
            return (QVTBase.IFunction)relationalTransformation.OwnedOperation.FirstOrDefault(o => o.Name == toString);
        }

        private void SetBindings(QVTRelations.IRelation relation, QVTBase.IPattern pattern, EssentialOCL.CSharpOpaqueExpression cSharpOpaqueExpression, ExpressionSyntax parsedExpression)
        {
            ISet<string> extractedIdentifiers = CSharpParser.ExtractNonMethodIdentifiersFromExpression(parsedExpression);
            foreach (string extractedIdentifier in extractedIdentifiers)
            {
                EssentialOCL.IVariable variable = ConstructVariable(relation, extractedIdentifier);
                pattern?.BindsTo.Add(variable);
                cSharpOpaqueExpression.BindsTo.Add(variable);
            }
        }

        private QVTBase.IPattern ConstructWhenOrWherePattern(QVTRelations.IRelation relation, string whenOrwhereCode)
        {
            QVTBase.Pattern result = new QVTBase.Pattern();

            List<StatementSyntax> instructions = CSharpParser.ParseInstructions(whenOrwhereCode);

            //TODO Filter ExpressionStatementSyntax is too restrictive, we must also create opaque expressions with eg. conditionnals
            foreach (ExpressionStatementSyntax expressionStatementSyntax in instructions.OfType<ExpressionStatementSyntax>())
            {
                ExpressionSyntax expression = expressionStatementSyntax.Expression;
                EssentialOCL.IOclExpression oclExpression = ConstructOCLExpression(relation, expression, result);
                QVTBase.IPredicate predicate = new QVTBase.Predicate()
                {
                    Pattern = result,
                    ConditionExpression = oclExpression,
                };
                result.Predicate.Add(predicate);
            }

            return result;
        }

        private QVTRelations.IRelation FindRelation(QVTRelations.IRelationalTransformation transformation, string relationName)
        {
            return (QVTRelations.IRelation)transformation.Rule.FirstOrDefault(r => r.Name == relationName);
        }

        private void ConstructBasicRelation(QVTRelations.IRelationalTransformation transformation, EnAr.Element relationElement)
        {
            // First we check if an empty relation doesn't already exist
            QVTRelations.IRelation relation = FindRelation(transformation, relationElement.Name);

            // If not, we create it
            if (relation == null)
            {
                relation = new QVTRelations.Relation()
                {
                    Name = relationElement.Name
                };
                transformation.Rule.Add(relation);

                // We find the unique child EA elements with the stereotype "qvtTransformationNode"
                List<EnAr.Element> transformationNodeElements = explorer.GetChildrenElementsWithTypeAndStereotype(relationElement, "class", "qvtTransformationNode");
                EnAr.Element transformationNodeElement = transformationNodeElements.Single();

                bool isTopRelation = explorer.GetTaggedValue(relationElement, "isTopRelation").ToLower() == "true";

                relation.IsTopLevel = isTopRelation;
                relation.Transformation = transformation;
                relation.Overrides = null; // TODO

                // We browse the EA Connectors with the stereotype "qvtTransformationLink", from the qvtTransformationNode
                // We use the concrete syntax extension to order the domains
                foreach (EnAr.Connector qvtTransformationLinkConnector in explorer.GetConnectorsWithSourceWithStereotype(transformationNodeElement, "qvtTransformationLink").OrderBy(c => c.Name))
                {
                    ConstructRelationDomain(transformation, relation, qvtTransformationLinkConnector);
                }
            }
        }

        private static ISet<EMOF.IClass> FindAllClassesUsedByObjectTemplate(QVTTemplate.IObjectTemplateExp objectTemplateExp, ISet<QVTTemplate.IObjectTemplateExp> alreadyDone = null)

        {
            ISet<EMOF.IClass> result = new HashSet<EMOF.IClass>();
            if (alreadyDone == null)
                alreadyDone = new HashSet<QVTTemplate.IObjectTemplateExp>();
            if (!alreadyDone.Contains(objectTemplateExp))
            {
                result.Add(objectTemplateExp.ReferredClass);
                foreach (QVTTemplate.IObjectTemplateExp templateExp in objectTemplateExp.Part.Select(p => p.Value).OfType<QVTTemplate.IObjectTemplateExp>())
                {
                    result.UnionWith(FindAllClassesUsedByObjectTemplate(templateExp, alreadyDone));
                }
            }
            return result;
        }

        private static ISet<EMOF.IClass> FindAllClassesUsedInRelation(QVTRelations.IRelation relation)
        {
            ISet<EMOF.IClass> result = new HashSet<EMOF.IClass>();
            foreach (QVTTemplate.IObjectTemplateExp objectTemplateExp in relation.Domain.OfType<QVTRelations.IRelationDomain>().Where(d => d.Pattern != null).Select(d => d.Pattern.TemplateExpression).OfType<QVTTemplate.IObjectTemplateExp>())
            {
                result.AddRange(FindAllClassesUsedByObjectTemplate(objectTemplateExp));
            }
            return result;
        }

        private void FinishRelation(QVTRelations.RelationalTransformation transformation, EnAr.Element relationElement)
        {
            // We use the existing Relation object
            QVTRelations.IRelation relation = FindRelation(transformation, relationElement.Name);

            // We look for the tag "where"
            string whereCode = explorer.GetTaggedValue(relationElement, "Where");
            if (!string.IsNullOrWhiteSpace(whereCode))
            {
                relation.Where = ConstructWhenOrWherePattern(relation, whereCode);
            }

            // We look for the tag "when"
            string whenCode = explorer.GetTaggedValue(relationElement, "When");
            if (!string.IsNullOrWhiteSpace(whenCode))
            {
                relation.When = ConstructWhenOrWherePattern(relation, whenCode);
            }

            // We look for the tag 'qvtKey'
            string qvtKeyString = explorer.GetTaggedValue(relationElement, "qvtKey");
            if (!string.IsNullOrWhiteSpace(qvtKeyString))
            {
                relationsWithKeys.Add(relation);
                ISet<EMOF.IClass> classes = FindAllClassesUsedInRelation(relation);
                IList<QvtKeyParserResult> parsed = QvtKeyParser.Parse(qvtKeyString);
                foreach (QvtKeyParserResult keyParserResult in parsed)
                {
                    QVTRelations.IKey key = ConstructKey(classes, keyParserResult);
                    QVTRelations.IKey existingKey = relation.Keys().FirstOrDefault(k => k.Identifies == key.Identifies);
                    if (existingKey == null)
                        relation.Keys().Add(key);
                    else
                        MergeKeyInto(existingKey, key);
                }
            }
        }

        private static bool AreEqual(PropertyPath p1, PropertyPath p2)
        {
            if (p1.Properties.Count == p2.Properties.Count)
            {
                foreach (EMOF.IProperty p1Property in p1.Properties)
                {
                    EMOF.IProperty p2Property = p2.Properties[p1.Properties.IndexOf(p1Property)];
                    if (p1Property != p2Property)
                        return false;
                }
            }
            else
            {
                return false;
            }
            return true;
        }

        private static void MergeKeyInto(QVTRelations.IKey existingKey, QVTRelations.IKey keyToBeMergedInto)
        {
            foreach (EMOF.IProperty propertyToMerge in keyToBeMergedInto.Part)
            {
                if (!existingKey.Part.Contains(propertyToMerge))
                {
                    existingKey.Part.Add(propertyToMerge);
                }
            }
            foreach (PropertyPath propertyPathToMerge in keyToBeMergedInto.PropertyPaths())
            {
                if (existingKey.PropertyPaths().FirstOrDefault(pp => AreEqual(pp, propertyPathToMerge)) == null)
                {
                    existingKey.PropertyPaths().Add(propertyPathToMerge);
                }
            }
        }

        private static QVTRelations.IKey ConstructKey(ISet<EMOF.IClass> reachableClasses, QvtKeyParserResult keyParserResult)
        {
            EMOF.IClass clazz = reachableClasses.Single(c => c.Name == keyParserResult.ClassName);
            QVTRelations.IKey key = new QVTRelations.Key
            {
                Identifies = clazz
            };

            foreach (IList<string> propertyPathList in keyParserResult.NavigatedProperties)
            {
                // Case multiple navigated properties, we use our QVT extension "PropertyPaths"
                if (propertyPathList.Count > 1)
                {
                    PropertyPath propertyPath = new PropertyPath();
                    EMOF.IClass currentClass = clazz;
                    foreach (string propertyName in propertyPathList)
                    {
                        if (currentClass == null)
                            throw new InvalidQVTRelationsModelException("Wrong key somewhere, impossible to reach: \'" + propertyName + "\'");
                        EMOF.IProperty nextProperty = currentClass.OwnedAttribute.Single(p => p.Name == propertyName);
                        if (nextProperty.isMany())
                        {
                            throw new InvalidQVTRelationsModelException("Wrong key somewhere, cannot use a collection as part of key for now: \'" + propertyName + "\'");
                        }
                        propertyPath.Properties.Add(nextProperty);
                        currentClass = nextProperty.Type as EMOF.IClass;
                    }
                    key.PropertyPaths().Add(propertyPath);
                }

                // Else, we fill the regular "part" of the key
                else
                {
                    EMOF.IProperty property = clazz.OwnedAttribute.Single(p => p.Name == propertyPathList.Single());
                    if (property.isMany())
                    {
                        throw new InvalidQVTRelationsModelException("Wrong key somewhere, cannot use a collection as part of key for now: \'" + property.Name + "\'");
                    }
                    key.Part.Add(property);
                }
            }
            return key;
        }

        private QVTBase.FunctionParameter ConstructParameter(QVTBase.Function function, EnAr.Parameter parameter)
        {
            QVTBase.FunctionParameter result = new QVTBase.FunctionParameter()
            {
                Operation = function,
                Type = emofImporter.ConstructTypeOfParameter(parameter),
                Name = parameter.Name,
                IsOrdered = null,
                IsUnique = null,
                Lower = null,
                Upper = null,
                InitExpression = null,
                RepresentedParameter = null,
                ReferencedElements = { },
                OwnedComment = { },
                LetExp = null,
            };

            function.OwnedParameter.Add(result);
            return result;
        }

        private QVTBase.Function ConstructFunction(QVTRelations.RelationalTransformation transformation, EnAr.Method method)
        {
            QVTBase.Function result = new QVTBase.Function()
            {
                Type = emofImporter.ConstructTypeOfMethod(method),
                Name = method.Name,
                Class = transformation,
                IsOrdered = false,
                IsUnique = false,
                Lower = 1,
                Upper = 1,
                OwnedComment = { },
                QueryExpression = null,
                RaisedException = { },
                ReferencedElements = { }, // TODO ???
            };

            foreach (EnAr.Parameter parameter in method.Parameters)
            {
                ConstructParameter(result, parameter);
            }

            return result;
        }

        private QVTRelations.IRelationalTransformation ConstructRelationalTransformation(EnAr.Element transformationElement)
        {
            // We create a Transformation object
            QVTRelations.RelationalTransformation transformation = new QVTRelations.RelationalTransformation
            {
                Name = transformationElement.Name,
            };

            // We first find the "Functions" class that contains the functions of the transformation
            EnAr.Element functionsClass = explorer.GetChildrenElementsWithType(transformationElement, "class").FirstOrDefault(e => e.Stereotype.IsNullOrEmpty());

            if (functionsClass != null)
            {
                // For each method, we create a Function in the QVT transforamtion
                foreach (EnAr.Method method in functionsClass.Methods.OfType<EnAr.Method>())
                {
                    ConstructFunction(transformation, method);
                }
            }

            // We browse the children EA elements with the stereotype "qvtRelation"
            IList<EnAr.Element> relationsElements = explorer.GetChildrenElementsWithTypeAndStereotype(transformationElement, "class", "qvtRelation");

            // First pass: we create the basic relations (to manage relation calls later)
            foreach (EnAr.Element relationElement in relationsElements)
            {
                ConstructBasicRelation(transformation, relationElement);
            }

            // Second pass, we finish the relations (with relation calls)
            foreach (EnAr.Element relationElement in relationsElements)
            {
                FinishRelation(transformation, relationElement);
            }

            // We look for the tag 'qvtKey'
            string qvtKeyString = explorer.GetTaggedValue(transformationElement, "qvtKey");
            if (!string.IsNullOrWhiteSpace(qvtKeyString))
            {
                ISet<EMOF.IClass> classes = FindAllClassesUsedInTransformation(transformation);
                IList<QvtKeyParserResult> parsed = QvtKeyParser.Parse(qvtKeyString);
                foreach (QvtKeyParserResult keyParserResult in parsed)
                {
                    QVTRelations.IKey key = ConstructKey(classes, keyParserResult);
                    QVTRelations.IKey existingKey = transformation.OwnedKey.FirstOrDefault(k => k.Identifies == key.Identifies);
                    if (existingKey == null)
                        transformation.OwnedKey.Add(key);
                    else
                        MergeKeyInto(existingKey, key);
                }
            }

            // We check if some relation-level keys should not be removed because subsumed by global keys
            foreach (QVTRelations.IRelation relationWithKeys in relationsWithKeys)
            {
                foreach (QVTRelations.IKey key in relationWithKeys.Keys().ToList())
                {
                    QVTRelations.IKey superKey = transformation.OwnedKey.FirstOrDefault(k => Subsumes(k, key));
                    if (superKey != null)
                    {
                        relationWithKeys.Keys().Remove(key);
                    }
                }
            }

            //TODO QVT Query Functions owned by the transformation?

            return transformation;
        }

        /// <summary>
        /// Checks if all properties in k1 are found in k2. If yes, k2 is useless.
        /// </summary>
        /// <param name="k1"></param>
        /// <param name="k2"></param>
        /// <returns></returns>
        private static bool Subsumes(QVTRelations.IKey k1, QVTRelations.IKey k2)
        {
            if (k1.Part.Count <= k2.Part.Count && k1.PropertyPaths().Count <= k2.PropertyPaths().Count)
            {
                return k1.Part.IsSubsetOf(k2.Part)
                       && k1.PropertyPaths().All(k1PropertyPath => k2.PropertyPaths()
                           .Any(k2PropertyPath => AreEqual(k1PropertyPath, k2PropertyPath)));
            }
            return false;
        }

        private static ISet<EMOF.IClass> FindAllClassesUsedInTransformation(QVTRelations.RelationalTransformation transformation)
        {
            ISet<EMOF.IClass> result = new HashSet<EMOF.IClass>();
            foreach (QVTRelations.IRelation relation in transformation.Rule.OfType<QVTRelations.IRelation>())
            {
                result.AddRange(FindAllClassesUsedInRelation(relation));
            }
            return result;
        }

        public QVTRelations.IRelationalTransformation ConstructRelationalTransformationFromGuid(string guid)
        {
            IList<EnAr.Element> transformationsElements = explorer.FindElementsWithTypeAndStereotype("component", "qvtTransformation");
            EnAr.Element transfo = transformationsElements.Single(e => e.ElementGUID == guid);
            QVTRelations.IRelationalTransformation transformation = ConstructRelationalTransformation(transfo);
            return transformation;
        }

        public QVTRelations.IRelationalTransformation ConstructRelationalTransformation(string transformatioName)
        {
            IList<EnAr.Element> transformationsElements = explorer.FindElementsWithTypeAndStereotype("component", "qvtTransformation");
            EnAr.Element transfo = transformationsElements.Single(e => e.Name == transformatioName);
            QVTRelations.IRelationalTransformation transformation = ConstructRelationalTransformation(transfo);
            return transformation;
        }

        public List<QVTRelations.IRelationalTransformation> ConstructRelationalTransformations()
        {
            // Future result: a list of qvt transformations
            List<QVTRelations.IRelationalTransformation> result = new List<QVTRelations.IRelationalTransformation>();

            // We browse in the EA repo all the elements with the "qvtTransformation" stereotype
            IList<EnAr.Element> transformationsElements = explorer.FindElementsWithTypeAndStereotype("component", "qvtTransformation");
            foreach (EnAr.Element transformationElement in transformationsElements)
            {
                QVTRelations.IRelationalTransformation transformation = ConstructRelationalTransformation(transformationElement);
                result.Add(transformation);
            }

            return result;
        }
    }
}