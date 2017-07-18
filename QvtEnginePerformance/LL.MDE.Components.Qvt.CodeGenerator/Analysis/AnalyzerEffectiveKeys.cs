using System.Collections.Generic;
using System.Linq;

using LL.MDE.Components.Qvt.Metamodel.CustomExtensions.QVTRelationExtensions;
using LL.MDE.Components.Qvt.Metamodel.EMOF;
using LL.MDE.Components.Qvt.Metamodel.QVTRelation;

using NMF.Utilities;

namespace LL.MDE.Components.Qvt.QvtCodeGenerator.Analysis
{
    public class AnalyzerEffectiveKeys
    {
        private static bool AreEqual(PropertyPath p1, PropertyPath p2)
        {
            if (p1.Properties.Count == p2.Properties.Count)
            {
                foreach (IProperty p1Property in p1.Properties)
                {
                    IProperty p2Property = p2.Properties[p1.Properties.IndexOf(p1Property)];
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

        private static bool AreEqual(IKey k1, IKey k2)
        {
            if (k1.Part.Count == k2.Part.Count && k1.PropertyPaths().Count == k2.PropertyPaths().Count)
            {
                if (!(k1.Part.IsSubsetOf(k2.Part) && k1.Part.IsSupersetOf(k2.Part)))
                {
                    return false;
                }
                foreach (PropertyPath k1PropertyPath in k1.PropertyPaths())
                {
                    if (k2.PropertyPaths().Where(k2PropertyPath => AreEqual(k1PropertyPath, k2PropertyPath)).IsNullOrEmpty())
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
            return true;
        }

        public static ISet<IKey> Run(IRelation relation)
        {
            // We prepare the result
            ISet<IKey> result = new HashSet<IKey>();

            // We get all keys
            ISet<IKey> allKeys = new HashSet<IKey>();
            allKeys.UnionWith(relation.Keys());
            allKeys.UnionWith(((IRelationalTransformation)relation.Transformation).OwnedKey);

            // We copy each key in the result if no equal key is present
            foreach (IKey key in allKeys)
            {
                IKey existingKey = result.FirstOrDefault(k => AreEqual(k, key));
                if (existingKey == null)
                {
                    result.Add(key);
                }
            }
            return result;
        }
    }
}