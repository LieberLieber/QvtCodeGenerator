using System;
using System.Collections.Generic;
using EA;
using LL.MDE.Components.Common.EnArLoader;
using Ecore = NMF.Interop.Ecore;
using EnAr = LL.MDE.DataModels.EnAr;

namespace LL.MDE.Components.XsdImport.Ecore2EnAr
{
    public class Ecore2EnAr
    {
        private readonly EnAr.Package rootContainerPackage;

        private readonly Dictionary<Ecore.IEPackage, EnAr.Package> ePackage2Package =
            new Dictionary<Ecore.IEPackage, EnAr.Package>();

        private readonly Dictionary<Ecore.IEClassifier, EnAr.Element> eclassifier2Class =
            new Dictionary<Ecore.IEClassifier, EnAr.Element>();

        private readonly Dictionary<Ecore.IEReference, Tuple<EnAr.Connector, EnAr.ConnectorEnd>> eReference2ConnectorEnd
            = new Dictionary<Ecore.IEReference, Tuple<EnAr.Connector, EnAr.ConnectorEnd>>();

        private readonly EnArExplorer explorer;

        public Ecore2EnAr(EnAr.Package rootContainerPackage, EnArExplorer explorer)
        {
            this.rootContainerPackage = rootContainerPackage;
            this.explorer = explorer;
        }

        private EnAr.Package ConstructMetamodelPackage(Ecore.IEPackage ePackage, bool constructContent = false)
        {
            if (!ePackage2Package.ContainsKey(ePackage))
            {
                EnAr.Package parentPackage = ePackage.ESuperPackage != null
                    ? ConstructMetamodelPackage(ePackage.ESuperPackage)
                    : rootContainerPackage;
                EnAr.Package package = (EnAr.Package) parentPackage.Packages.AddNew(ePackage.Name, "Package");
                ePackage2Package[ePackage] = package;
                package.Element.Stereotype = "metamodel";
                parentPackage.Packages.Refresh();
                parentPackage.Update();
                package.Update();
                package.Element.Update();
            }
            if (constructContent)
            {
                foreach (Ecore.IEClassifier eClassifier in ePackage.EClassifiers)
                {
                    ConstructClassifier(eClassifier);
                }

                foreach (Ecore.IEPackage eSubpackage in ePackage.ESubpackages)
                {
                    ConstructMetamodelPackage(eSubpackage, true);
                }
            }

            return ePackage2Package[ePackage];
        }

        private static string Bool2String(bool b)
        {
            return b ? "True" : "False";
        }

        private EnAr.Element ConstructClassifier(Ecore.IEClassifier eClassifier)
        {
            if (eclassifier2Class.ContainsKey(eClassifier))
            {
                return eclassifier2Class[eClassifier];
            }

            EnAr.Element result;

            if (eClassifier is Ecore.IEClass)
            {
                result = ConstructClass((Ecore.IEClass) eClassifier);
            }
            else if (eClassifier is Ecore.IEEnum)
            {
                result = ConstructEnumeration((Ecore.IEEnum) eClassifier);
            }
            else
            {
                result = ConstructPrimitiveType((Ecore.IEDataType) eClassifier);
            }

            EA.Element clazzEa = explorer.GetEaObject(result);

            // Manage type parameters (eg. T in MyClass<T>)
            foreach (Ecore.IETypeParameter eTypeParameter in eClassifier.ETypeParameters)
            {
                dynamic eaTemplateParameter = clazzEa.TemplateParameters.AddNew(eTypeParameter.Name, "");
                eaTemplateParameter.Constraint = "";
                List<string> guids = new List<string>();
                foreach (Ecore.IEGenericType eGenericType in eTypeParameter.EBounds)
                {
                    EnAr.Element constraintClass = ConstructClassifier(eGenericType.EClassifier);
                    guids.Add(constraintClass.ElementGUID);
                }
                eaTemplateParameter.Constraint = string.Join(",", guids);
                eaTemplateParameter.Update();
            }
            clazzEa.TemplateParameters.Refresh();
            clazzEa.Refresh();
            clazzEa.Update();
            return result;
        }

        private EnAr.Element ConstructEnumeration(Ecore.IEEnum eEnum)
        {
            if (eclassifier2Class.ContainsKey(eEnum))
                return eclassifier2Class[eEnum];

            EnAr.Package parentPackage = ConstructMetamodelPackage(eEnum.EPackage);
            EnAr.Element enumeration = (EnAr.Element) parentPackage.Elements.AddNew(eEnum.Name, "Enumeration");
            eclassifier2Class[eEnum] = enumeration;

            foreach (Ecore.IEEnumLiteral eEnumLiteral in eEnum.ELiterals)
            {
                EnAr.Attribute attributeLiteral =
                    (EnAr.Attribute) enumeration.Attributes.AddNew(eEnumLiteral.Name, "Attribute");
                attributeLiteral.Stereotype = "enum";
                attributeLiteral.LowerBound = "1";
                attributeLiteral.UpperBound = "1";
                attributeLiteral.IsCollection = false;
                attributeLiteral.Update();
            }
            enumeration.Attributes.Refresh();
            enumeration.Update();
            enumeration.Refresh();
            return enumeration;
        }


        private EnAr.Connector ConstructGeneralization(Ecore.IEClass superEClass, EnAr.Element clazz)
        {
            EnAr.Element superClass = ConstructClassifier(superEClass);
            EnAr.Connector inheritanceConnector = (EnAr.Connector) clazz.Connectors.AddNew("", "Connector");
            inheritanceConnector.Type = "Generalization";
            inheritanceConnector.ClientID = clazz.ElementID;
            inheritanceConnector.SupplierID = superClass.ElementID;
            inheritanceConnector.Update();
            return inheritanceConnector;
        }

        private EnAr.Element ConstructClass(Ecore.IEClass eClass)
        {
            EnAr.Package parentPackage;
            EnAr.Element clazz;

            if (!eclassifier2Class.ContainsKey(eClass))
            {
                parentPackage = ConstructMetamodelPackage(eClass.EPackage);
                clazz = (EnAr.Element) parentPackage.Elements.AddNew(eClass.Name, "Class");
                eclassifier2Class[eClass] = clazz;
                clazz.Abstract = Bool2String(eClass.Abstract.GetValueOrDefault() || eClass.Interface.GetValueOrDefault());


                // Manage normal super types
                foreach (Ecore.IEClass superEClass in eClass.ESuperTypes)
                {
                    ConstructGeneralization(superEClass, clazz);
                }

                // Manage super types that possess type parameters
                foreach (Ecore.IEGenericType eGenericSuperType in eClass.EGenericSuperTypes)
                {
                    if (eGenericSuperType.EClassifier != null)
                    {
                        EnAr.Connector generalization =
                            ConstructGeneralization((Ecore.IEClass) eGenericSuperType.EClassifier, clazz);
                        EA.Connector generalizationEa = explorer.GetEaObject(generalization);
                        foreach (Ecore.IEGenericType eTypeArgument in eGenericSuperType.ETypeArguments)
                        {
                            ConstructTemplateBinding(eGenericSuperType, eTypeArgument, generalizationEa);
                        }
                    }
                }


                foreach (Ecore.IEStructuralFeature eStructuralFeature in eClass.EStructuralFeatures)
                {
                    ConstructAttributeOrAssociation(eclassifier2Class[eClass], eStructuralFeature);
                }


                // eClass.EOperations //TODO
            }
            else
            {
                parentPackage = ePackage2Package[eClass.EPackage];
                clazz = eclassifier2Class[eClass];
            }

            parentPackage.Elements.Refresh();
            parentPackage.Update();
            clazz.Update();
            clazz.Connectors.Refresh();
            return clazz;
        }

        private void ConstructAttributeOrAssociation(EnAr.IElement clazz, Ecore.IEStructuralFeature eStructuralFeature)
        {
            if (eStructuralFeature is Ecore.IEAttribute || eStructuralFeature.EGenericType?.ETypeParameter != null)
            {
                ConstructAttribute(clazz, eStructuralFeature);
            }
            else
            {
                ConstructConnectorEnd(clazz, (Ecore.IEReference) eStructuralFeature);
            }
        }

        private void ConstructConnectorEnd(EnAr.IElement clazz, Ecore.IEReference eReference)
        {
            EnAr.Connector connector;
            EnAr.ConnectorEnd connectorEnd;
            EnAr.ConnectorEnd otherConnectorEnd;

            // First we find the type pointed by the reference
            Ecore.IEClass ecoreType = null;
            if (eReference.EType != null)
            {
                ecoreType = (Ecore.IEClass) eReference.EType;
            }
            else if (eReference.EGenericType != null)
            {
                ecoreType = (Ecore.IEClass) eReference.EGenericType.EClassifier;
            }


            // If no connector end managed yet, we set up everything
            if (!eReference2ConnectorEnd.ContainsKey(eReference))
            {
                connector = (EnAr.Connector) clazz.Connectors.AddNew("", "Connector");
                connector.ClientID = clazz.ElementID;
                connectorEnd = connector.SupplierEnd;
                eReference2ConnectorEnd[eReference] = new Tuple<EnAr.Connector, EnAr.ConnectorEnd>(connector,
                    connectorEnd);
                otherConnectorEnd = connector.ClientEnd;

                // Managing navigability / direction
                connectorEnd.IsNavigable = true;
                connectorEnd.Navigable = "Navigable";
                otherConnectorEnd.IsNavigable = false;
                otherConnectorEnd.Navigable = "Non-Navigable";
                connector.Direction = "Source -> Destination";

                // In case of opposite, we manage the other connector end to have everything bi directionnal
                if (eReference.EOpposite != null)
                {
                    otherConnectorEnd.IsNavigable = true;
                    otherConnectorEnd.Navigable = "Navigable";
                    connector.Direction = "Bi-Directional";
                    eReference2ConnectorEnd[eReference.EOpposite] =
                        new Tuple<EnAr.Connector, EnAr.ConnectorEnd>(connector, otherConnectorEnd);
                }

                EnAr.Element pointedClass = ConstructClassifier(ecoreType);
                connector.SupplierID = pointedClass.ElementID;
            }

            // If there is already a connector end managed, we retrieve it
            else
            {
                connector = eReference2ConnectorEnd[eReference].Item1;
                connectorEnd = eReference2ConnectorEnd[eReference].Item2;

                if (connectorEnd.End == "ClientEnd")
                {
                    otherConnectorEnd = connector.SupplierEnd;
                }
                else if (connectorEnd.End == "SupplierEnd")
                {
                    otherConnectorEnd = connector.ClientEnd;
                }
                else
                {
                    throw new Exception();
                }
            }

            // Containment must be set on the opposite end...
            otherConnectorEnd.Aggregation = eReference.Containment.GetValueOrDefault() ? 2 : 0;
            connector.Type = eReference.Containment.GetValueOrDefault() ? "Aggregation" : "Association";

            // Everything else is on the same end
            connectorEnd.Role = eReference.Name;
            connectorEnd.Cardinality = BoundsToString(eReference.LowerBound.GetValueOrDefault(),
                eReference.UpperBound.GetValueOrDefault());
            connectorEnd.Derived = eReference.Derived.GetValueOrDefault();
            if (eReference.UpperBound.GetValueOrDefault() == -1 || eReference.UpperBound.GetValueOrDefault() > 1)
            {
                connectorEnd.Ordering = eReference.Ordered.GetValueOrDefault() ? 1 : 0;
            }
            connectorEnd.AllowDuplicates = !eReference.Unique.GetValueOrDefault();

            // Finally we set the type arguments, if any (eg. T = Toto for type MyClass<T>)
            Ecore.IEGenericType eGenericType = eReference.EGenericType;
            if (eGenericType?.EClassifier != null)
            {
                if (connectorEnd.End == "SupplierEnd")
                {
                    Connector connectorEa = explorer.GetEaObject(connector);
                    foreach (Ecore.IEGenericType ecoreTypeArgument in eGenericType.ETypeArguments)
                    {
                        ConstructTemplateBinding(eGenericType, ecoreTypeArgument, connectorEa);
                    }
                    connectorEa.TemplateBindings.Refresh();
                }
            }

            connector.Update();
            connectorEnd.Update();
            otherConnectorEnd.Update();
            clazz.Connectors.Refresh();
            clazz.Update();
        }

        private void ConstructTemplateBinding(Ecore.IEGenericType eGenericType,
            Ecore.IEGenericType ecoreTypeArgument, EA.Connector connectorEa)
        {
            int positionTypeArgument = eGenericType.ETypeArguments.IndexOf(ecoreTypeArgument);
            Ecore.IETypeParameter typeParameter =
                eGenericType.EClassifier.ETypeParameters[positionTypeArgument];

            string bindedName = ecoreTypeArgument.EClassifier.Name;
            EnAr.Element bindedClass = ConstructClassifier(ecoreTypeArgument.EClassifier);
            dynamic templateBinding = connectorEa.TemplateBindings.AddNew(bindedName, "");
            templateBinding.formalname = typeParameter.Name;
            templateBinding.ActualGUID = bindedClass.ElementGUID;
            try
            {
                templateBinding.Update();
            }
            catch (Exception)
            {
                // ignored
            }
            connectorEa.Update();
        }


        private static string BoundsToString(int lower, int upper)
        {
            string start = lower.ToString();
            string end = upper.ToString();
            if (lower == 0 && upper == -1)
                return "*";
            if (lower == 1 && upper == 1)
                return "1";
            if (upper == -1)
                end = "*";
            return start + ".." + end;
        }

        private static string BoundToString(int bound)
        {
            return bound == -1 ? "*" : bound.ToString();
        }

        private void ConstructAttribute(EnAr.IElement clazz, Ecore.IEStructuralFeature eStructuralFeature)
        {
            EnAr.Attribute attribute = (EnAr.Attribute) clazz.Attributes.AddNew(eStructuralFeature.Name, "Attribute");

            // Case regular type without template
            if (eStructuralFeature.EType != null)
            {
                EnAr.Element attributeType = ConstructClassifier(eStructuralFeature.EType);
                attribute.ClassifierID = attributeType.ElementID;
                attribute.Type = attributeType.Name;
            }

            else if (eStructuralFeature.EGenericType != null)
            {
                Ecore.IEGenericType eGenericType = eStructuralFeature.EGenericType;

                // Case templated type (eg. MyClass<T>) 
                if (eGenericType.EClassifier != null)
                {
                    EnAr.Element attributeType = ConstructClassifier(eGenericType.EClassifier);
                    attribute.ClassifierID = attributeType.ElementID;
                    attribute.Type = attributeType.Name;
                    foreach (Ecore.IEGenericType eTypeArgument in eGenericType.ETypeArguments)
                    {
                        //TODO impossible to manage local bindings with regular attribute in EA, needs to be a reference/association?
                    }
                }

                // Case template type (eg. T), we simply set the same name 
                else if (eGenericType.ETypeParameter != null)
                {
                    // TODO can we do better than that in EA?
                    attribute.Type = eGenericType.ETypeParameter.Name;
                }
            }


            attribute.LowerBound = BoundToString(eStructuralFeature.LowerBound.GetValueOrDefault());
            attribute.UpperBound = BoundToString(eStructuralFeature.UpperBound.GetValueOrDefault());

            if (eStructuralFeature.UpperBound.GetValueOrDefault() == -1 ||
                eStructuralFeature.UpperBound.GetValueOrDefault() > 1)
            {
                attribute.IsOrdered = eStructuralFeature.Ordered.GetValueOrDefault();
            }

            attribute.AllowDuplicates = !eStructuralFeature.Unique.GetValueOrDefault();
            attribute.IsDerived = eStructuralFeature.Derived.GetValueOrDefault();
            attribute.IsConst = !eStructuralFeature.Changeable.GetValueOrDefault();
            attribute.IsCollection = eStructuralFeature.UpperBound == -1 || eStructuralFeature.UpperBound > 1;
            attribute.Default = eStructuralFeature.DefaultValueLiteral;

            clazz.Attributes.Refresh();
            attribute.Update();
        }

        private readonly Dictionary<string, string> ecorePrimitiveToEnAr = new Dictionary<string, string>()
        {
            {"EInt", "int"},
            {"EInteger", "int"},
            {"EString", "string"},
            {"EBool", "bool"},
            {"EBoolean", "bool"},
            {"ELong", "long"},
            {"EFloat", "float"},
        };

        private EnAr.Element ConstructPrimitiveType(Ecore.IEDataType eDataType)
        {
            if (!eclassifier2Class.ContainsKey(eDataType))
            {
                EnAr.Package parentPackage = ConstructMetamodelPackage(eDataType.EPackage);
                EnAr.Element primitiveType = (EnAr.Element) parentPackage.Elements.AddNew(eDataType.Name, "Class");
                eclassifier2Class[eDataType] = primitiveType;
                primitiveType.Stereotype = "primitive";

                if (ecorePrimitiveToEnAr.ContainsKey(eDataType.Name))
                {
                    EnAr.TaggedValue tag =
                        (EnAr.TaggedValue) primitiveType.TaggedValues.AddNew("MOFPrimitiveType", "Tag");
                    tag.Value = ecorePrimitiveToEnAr[eDataType.Name];
                    tag.Update();
                    primitiveType.TaggedValues.Refresh();
                }

                parentPackage.Elements.Refresh();
                parentPackage.Update();
                parentPackage.Element.Update();
                primitiveType.Update();
                return primitiveType;
            }
            return eclassifier2Class[eDataType];
        }

        public void ConstructMetamodel(ISet<Ecore.IEPackage> ePackages)
        {
            foreach (Ecore.IEPackage ePackage in ePackages)
            {
                ConstructMetamodelPackage(ePackage, true);
            }
            rootContainerPackage.Elements.Refresh();
            rootContainerPackage.Packages.Refresh();
            rootContainerPackage.Element.Update();
            rootContainerPackage.Update();
        }
    }
}