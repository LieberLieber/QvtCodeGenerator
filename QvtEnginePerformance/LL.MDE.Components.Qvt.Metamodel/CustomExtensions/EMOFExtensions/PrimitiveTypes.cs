using LL.MDE.Components.Qvt.Metamodel.EMOF;

namespace LL.MDE.Components.Qvt.Metamodel.EMOFExtensions
{
    public static class PrimitiveTypes
    {
        public static readonly IPrimitiveType BOOLEAN = new PrimitiveType() { Name = "boolean" };
        public static readonly IPrimitiveType INTEGER = new PrimitiveType() { Name = "integer" };
        public static readonly IPrimitiveType REAL = new PrimitiveType() { Name = "real" };
        public static readonly IPrimitiveType STRING = new PrimitiveType() {Name = "string"};
        public static readonly IPrimitiveType UNLIMITEDNATURAL = new PrimitiveType() { Name = "unlimitednatural" };
    }
}