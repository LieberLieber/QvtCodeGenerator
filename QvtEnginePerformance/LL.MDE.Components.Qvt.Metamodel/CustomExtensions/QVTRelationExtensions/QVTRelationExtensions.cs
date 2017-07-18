using System.Collections.Generic;

using LL.MDE.Components.Qvt.Metamodel.QVTRelation;

namespace LL.MDE.Components.Qvt.Metamodel.CustomExtensions.QVTRelationExtensions
{
    public static class QVTRelationExtensions
    {
        private static readonly Dictionary<IRelation, ISet<IKey>> allRelationsKeys = new Dictionary<IRelation, ISet<IKey>>();
        private static readonly Dictionary<IKey, ISet<PropertyPath>> allKeysPropetryPaths = new Dictionary<IKey, ISet<PropertyPath>>();

        public static ISet<IKey> Keys(this IRelation relation)
        {
            if (!allRelationsKeys.ContainsKey(relation))
            {
                allRelationsKeys[relation] = new HashSet<IKey>();
            }
            return allRelationsKeys[relation];
        }

        public static ISet<PropertyPath> PropertyPaths(this IKey key)
        {
            if (!allKeysPropetryPaths.ContainsKey(key))
            {
                allKeysPropetryPaths[key] = new HashSet<PropertyPath>();
            }
            return allKeysPropetryPaths[key];
        }
    }
}