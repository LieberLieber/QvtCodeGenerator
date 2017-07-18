using System.Collections.Generic;
using System.Linq;

using LL.MDE.Components.Qvt.Metamodel.EMOF;

using NMF.Utilities;

namespace LL.MDE.Components.Qvt.Metamodel.CustomExtensions.EMOFExtensions
{
	public static class ClassExtensions
	{
		public static ISet<IProperty> GetAllInheritedAttributes(this IClass c)
		{
			ISet<IProperty> result = new HashSet<IProperty>();
			result.UnionWith(c.OwnedAttribute);
			foreach (IClass superType in c.SuperClass)
			{
				result.UnionWith(superType.GetAllInheritedAttributes());
			}
			return result;
		}

		public static string ToString(this IClass cl)
		{
			return "Class \'" + cl.Name + "\' (isAbstract= " + cl.IsAbstract + ", superClass= [" + string.Join(";", cl.SuperClass.Select(c => c.ToString())) + "])";
		}

	    public static ISet<IClass> GetAllSubTypes(this IClass c) 
	    {
            return GetAllSubTypes(c, GetAllClasses(c));
        }

        private static ISet<IClass> GetAllSubTypes(IClass clazz, ISet<IClass> allClasses)
        {
            ISet<IClass> result = new HashSet<IClass>();
            result.Add(clazz);
            foreach (IClass otherClass in allClasses.Where(c=> c.SuperClass.Contains(clazz)))
            {
                result.Add(otherClass);
                result.AddRange(GetAllSubTypes(otherClass,allClasses));
            }

            return result;
        }

        private static ISet<IClass> GetAllClasses(IClass c)
        {
            IPackage rootPackage = GetRootPackage(c.Package);
            return GetAllClasses(rootPackage);
        }

        private static ISet<IClass> GetAllClasses(IPackage p)
        {
            ISet<IClass> result = new HashSet<IClass>();
            result.AddRange(p.OwnedType.OfType<IClass>());
            foreach (IPackage package in p.NestedPackage)
            {
                result.AddRange(GetAllClasses(package));
            }
            return result;
        }

	    private static IPackage GetRootPackage(IPackage p)
	    {
	        if (p.NestingPackage != null)
	        {
	            return GetRootPackage(p.NestingPackage);
	        }
	        else
	        {
	            return p;
	        }
	    }
    }

	public static class TypeExtensions
	{

		public static string GetRootPackageName(this IPackage package)
		{
			if (package.NestingPackage != null)
			{
				return package.NestingPackage.GetRootPackageName();
			}
			return package.Name;
		}

		public static string GetFQN(this IPackage package)
		{
			if (package.NestingPackage != null)
			{
				return package.NestingPackage.GetFQN() + "." + package.Name;
			}
			return package.Name;
		}

		public static string GetFQN(this IClass clazz)
		{
			if (clazz.Package != null)
			{
				return clazz.Package.GetRootPackageName() + "." + clazz.Name;
			}
			return clazz.Name;
		}

		public static string GetRealTypeName(this IType type)
		{
			IPrimitiveType primitiveType = type as IPrimitiveType;
			if (primitiveType != null)
			{
				string csharptype = primitiveType.GetCsharpType();
				if (!csharptype.IsNullOrEmpty())
					return csharptype;
			}
			IClass clazz = type as IClass;
			if (clazz != null)
			{
				return clazz.GetFQN();
			}
			return type.Name;
		}
	}

	public static class PrimitiveTypesExtensions
	{
		private static readonly Dictionary<IPrimitiveType, string> allCsharpTypes = new Dictionary<IPrimitiveType, string>();

		public static string GetCsharpType(this IPrimitiveType pt)
		{
			return allCsharpTypes.ContainsKey(pt) ? allCsharpTypes[pt] : null;
		}

		public static void SetCsharpType(this IPrimitiveType pt, string value)
		{
			allCsharpTypes[pt] = value;
		}
	}

	public static class PropertyExtensions
	{
		public static bool isMany(this IProperty p)
		{
			return p.Upper == -1 || p.Upper > 1;
		}

		public static string ToString(this IProperty p)
		{
			return "Property \'" + p.Name + "\' (type= " + p.Type + ", isComposite= " + p.IsComposite + ")";
		}
	}
}