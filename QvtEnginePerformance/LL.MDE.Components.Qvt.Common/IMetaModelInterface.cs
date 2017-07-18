namespace LL.MDE.Components.Qvt.Common
{
    public interface IMetaModelInterface
    {
        void AddOrSetInField(object element, string fieldName, object value);
        object CreateNewObjectInField(object element, string fieldName);

    }
}
