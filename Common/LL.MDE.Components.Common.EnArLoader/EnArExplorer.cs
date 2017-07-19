using System;
using System.Collections.Generic;
using EnAr = LL.MDE.DataModels.EnAr;

namespace LL.MDE.Components.Common.EnArLoader
{
    public class EnArExplorer
    {
        public readonly EnAr.Repository repository;
        private readonly EA.Repository repositoryEa;

        public EnArExplorer(EnAr.Repository repository, EA.Repository repositoryEA)
        {
            if (repositoryEA == null) throw new ArgumentNullException(nameof(repositoryEA));
            this.repository = repository;
            this.repositoryEa = repositoryEA;
        }

        public static bool EqualsNoCase(string s1, string s2)
        {
            return string.Equals(s1, s2, StringComparison.CurrentCultureIgnoreCase);
        }

        public EnAr.Package GetPackageByGuid(string guid)
        {
            EnAr.Element element = repository.GetElementByGuid(guid);
            return FindPackage(element);
        }

        public List<EnAr.Package> FindPackagesWithStereotype(string stereotype)
        {
            return
                repository.AllPackages.FindAll(p => p?.Element != null && EqualsNoCase(p.Element.Stereotype, stereotype));
        }

        public List<EnAr.Element> FindElementsWithType(string type)
        {
            return repository.AllElements.FindAll(e => EqualsNoCase(e.Type, type));
        }

        public List<EnAr.Element> FindElementsWithTypeAndStereotype(string type, string stereotype)
        {
            return FindElementsWithType(type).FindAll(e => EqualsNoCase(e.Stereotype, stereotype));
        }

        public EnAr.Package FindPackage(EnAr.Element element)
        {
            return repository.AllPackages.Find(p => p?.Element?.ElementID == element.ElementID);
        }

        public List<EnAr.Element> GetChildrenElements(EnAr.Package package)
        {
            List<EnAr.Element> result = new List<EnAr.Element>();
            foreach (object e in package.Elements)
            {
                if (e is EnAr.Element)
                    result.Add(e as EnAr.Element);
            }
            return result;
        }

        public EnAr.Element GetClassElement(EnAr.Element typedElement)
        {
            return FindElementsWithType("class").Find(c => c.ElementID == typedElement.ClassifierID);
        }

        public EnAr.Element GetClassElement(EnAr.Attribute typedAttribute)
        {
            return FindElementsWithType("class").Find(c => c.ElementID == typedAttribute.ClassifierID);
        }

        public List<EnAr.Package> GetChildrenPackages(EnAr.Package package)
        {
            return repository.AllPackages.FindAll(p => p.ParentID == package.PackageID);
        }

        public List<EnAr.Element> GetChildrenElementsWithType(EnAr.Package package, string type)
        {
            return GetChildrenElements(package).FindAll(e => EqualsNoCase(e.Type, type));
        }

        public List<EnAr.Element> GetChildrenElementsWithTypeAndStereotype(EnAr.Package package, string type,
            string stereotype)
        {
            return GetChildrenElementsWithType(package, type).FindAll(e => EqualsNoCase(e.Stereotype, stereotype));
        }

        public List<EnAr.Element> GetChildrenElements(EnAr.Element element)
        {
            if (EqualsNoCase(element.Type, "package"))
            {
                return GetChildrenElements(FindPackage(element));
            }
            else
            {
                return repository.AllElements.FindAll(e => e.ParentID == element.ElementID);
            }
        }

        public List<EnAr.Element> GetChildrenElementsWithType(EnAr.Element element, string type)
        {
            List<EnAr.Element> children = GetChildrenElements(element);
            return children.FindAll(e => EqualsNoCase(e.Type, type));
        }

        public List<EnAr.Element> GetChildrenElementsWithTypeAndStereotype(EnAr.Element element, string type,
            string stereotype)
        {
            List<EnAr.Element> childrenWithType = GetChildrenElementsWithType(element, type);
            return childrenWithType.FindAll(e => EqualsNoCase(e.Stereotype, stereotype));
        }

        public List<EnAr.Attribute> GetAttributes(EnAr.Element classElement)
        {
            List<EnAr.Attribute> result = new List<EnAr.Attribute>();
            foreach (object attribute in classElement.Attributes)
            {
                if (attribute is EnAr.Attribute)
                    result.Add(attribute as EnAr.Attribute);
            }
            return result;
        }

        public List<EnAr.Connector> GetConnectorsLinkedTo(EnAr.Element element)
        {
            return
                repository.AllConnectors.FindAll(
                    c => c.ClientID == element.ElementID || c.SupplierID == element.ElementID);
        }

        public Tuple<EnAr.ConnectorEnd, EnAr.Element> GetElementOppositeTo(EnAr.Element oneElement,
            EnAr.Connector connector)
        {
            if (connector.ClientID == oneElement.ElementID)
            {
                return new Tuple<EnAr.ConnectorEnd, EnAr.Element>(connector.SupplierEnd, GetTargetElement(connector));
            }
            if (connector.SupplierID == oneElement.ElementID)
            {
                return new Tuple<EnAr.ConnectorEnd, EnAr.Element>(connector.ClientEnd, GetSourceElement(connector));
            }
            throw new Exception("The provided element is not connected to the provided connector.");
        }

        public List<EnAr.Connector> GetConnectorsWithSource(EnAr.Element element)
        {
            return repository.AllConnectors.FindAll(c => c.ClientID == element.ElementID);
        }

        public List<EnAr.Connector> GetConnectorsWithSourceWithStereotype(EnAr.Element element, string stereotype)
        {
            return GetConnectorsWithSource(element).FindAll(c => EqualsNoCase(c.Stereotype, stereotype));
        }

        public EnAr.Element GetTargetElement(EnAr.Connector c)
        {
            return repository.AllElements.Find(element => c.SupplierID == element.ElementID);
        }

        public EnAr.Element GetSourceElement(EnAr.Connector c)
        {
            return repository.AllElements.Find(element => c.ClientID == element.ElementID);
        }

        public List<EnAr.Method> GetMethods(EnAr.Element e)
        {
            List<EnAr.Method> result = new List<EnAr.Method>();
            foreach (object method in e.Methods)
            {
                if (method is EnAr.Method)
                    result.Add(method as EnAr.Method);
            }
            return result;
        }

        public string GetTaggedValue(EnAr.Element element, String tagName)
        {
            foreach (EnAr.TaggedValue taggedValue2 in element.TaggedValues)
            {
                if (EqualsNoCase(taggedValue2.Name, tagName))
                {
                    return taggedValue2.Value;
                }
            }
            return null;
        }

        public string GetTaggedValue(EnAr.Connector connector, string tagName)
        {
            foreach (EnAr.ConnectorTag taggedValue in connector.TaggedValues)
            {
                if (EqualsNoCase(taggedValue.Name, tagName))
                {
                    return taggedValue.Value;
                }
            }
            return null;
        }

        public static List<RunStateField> GetRunState(EnAr.Element element)
        {
            List<RunStateField> result = new List<RunStateField>();
            string[] tokens = element.RunState.Split(';');

            const string variableString = "Variable";
            const string valueString = "Value";
            const string opString = "Op";
            const string noteString = "Note";

            RunStateField currentRunStateField = null;
            foreach (string token in tokens)
            {
                if (token == "@VAR")
                {
                    currentRunStateField = new RunStateField();
                }
                else if (token == "@ENDVAR")
                {
                    result.Add(currentRunStateField);
                }
                else if (token.StartsWith(variableString))
                {
                    if (currentRunStateField != null)
                        currentRunStateField.Variable = token.Substring(variableString.Length + 1);
                }
                else if (token.StartsWith(valueString))
                {
                    if (currentRunStateField != null)
                        currentRunStateField.Value = token.Substring(valueString.Length + 1);
                }
                else if (token.StartsWith(opString))
                {
                    if (currentRunStateField != null)
                        currentRunStateField.Operator = token.Substring(opString.Length + 1);
                }
                else if (token.StartsWith(noteString))
                {
                    if (currentRunStateField != null)
                        currentRunStateField.Notes = token.Substring(noteString.Length + 1);
                }
            }
            return result;
        }

        public EA.Element GetEaObject(EnAr.Element element)
        {
            return repositoryEa.GetElementByGuid(element.ElementGUID);
        }

        public EA.Connector GetEaObject(EnAr.Connector connector)
        {
            return repositoryEa.GetConnectorByGuid(connector.ConnectorGUID);
        }

        public EA.Package GetEaObject(EnAr.Package package)
        {
            return repositoryEa.GetPackageByGuid(package.PackageGUID);
        }

    }
}