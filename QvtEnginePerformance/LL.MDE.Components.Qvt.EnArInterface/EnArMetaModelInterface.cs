using System.Reflection;

using LL.MDE.Components.Qvt.Common;
using LL.MDE.DataModels.EnAr;

namespace LL.MDE.Components.Qvt.EnArInterface
{
    public class EnArMetaModelInterface : IMetaModelInterface
    {
        public object CreateNewObjectInField(object element, string fieldName)
        {
            object result = null;

            switch (fieldName)
            {
                case "Packages":
                    Package pHybrid = (Package)element;
                    result = pHybrid.Packages.AddNew("NewPackage", "Package");
                    pHybrid.Packages.Refresh();
                    break;
                case "Elements":
                    Package elementPackage = (Package)element;
                    result = elementPackage.Elements.AddNew("NewElement", "Class");
                    elementPackage.Elements.Refresh();
                    break;
                case "Connectors":
                    Element connectorElement = (Element)element;
                    result = connectorElement.Connectors.AddNew("NewConnector", "Connector");
                    connectorElement.Connectors.Refresh();
                    break;
            }

            return result;
        }

        public void AddOrSetInField(object element, string fieldName, object value)
        {
            
            PropertyInfo memberPropertyInfo = element.GetType().GetProperty(fieldName);
            memberPropertyInfo.SetValue(element, value, null);
            object[] args = new object[0];
            element.GetType().InvokeMember("Update", BindingFlags.Default | BindingFlags.InvokeMethod, null, element, args);
        }
    }
}