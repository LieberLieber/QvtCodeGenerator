using System.Linq;

// ReSharper disable once CheckNamespace
namespace LL.MDE.DataModels.EnAr
{
    public static class EnArExtensions
    {
        public static Package ParentPackage(this Package package)
        {
            return package.Repository.AllPackages.Single(p => p.PackageID == package.ParentID);
        }

        public static Element SourceElement(this Connector connector)
        {
            return connector.Repository.AllElements.Single(element => connector.ClientID == element.ElementID);
        }

        public static Element Classifier(this Element element)
        {
            return element.Repository.AllElements
                .FindAll(e => e.Type.ToLower() == "class")
                .Single(c => c.ElementID == element.ClassifierID);
        }

        public static Package ElementPackage(this Element element)
        {
            return element.Repository.AllPackages.Single(p => p.Element != null && p.Element.ElementID == element.ElementID);
        }
    }
}