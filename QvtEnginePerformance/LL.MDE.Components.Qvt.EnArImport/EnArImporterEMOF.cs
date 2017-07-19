using System;
using System.Collections.Generic;
using System.Linq;

using LL.MDE.Components.Common.EnArLoader;
using LL.MDE.Components.Qvt.Metamodel.CustomExtensions.EMOFExtensions;
using LL.MDE.Components.Qvt.Metamodel.EMOFExtensions;

using NMF.Utilities;

using EMOF = LL.MDE.Components.Qvt.Metamodel.EMOF;
using EnAr = LL.MDE.DataModels.EnAr;

namespace LL.MDE.Components.Qvt.EnArImport
{
	public class EnArImporterEMOF
	{
		private readonly Dictionary<EnAr.Package, EMOF.IPackage> elementToPackage = new Dictionary<EnAr.Package, EMOF.IPackage>();
		private readonly Dictionary<EnAr.Element, EMOF.IType> elementToType = new Dictionary<EnAr.Element, EMOF.IType>();
		private readonly EnArExplorer explorer;

		public EnArImporterEMOF(EnArExplorer explorer)
		{
			this.explorer = explorer;
		}

		private EMOF.IPrimitiveType ConstructPrimitiveType(EnAr.Element classElement)
		{
			EMOF.IPrimitiveType result = new EMOF.PrimitiveType()
			{
				Name = classElement.Name,
			};

			string mofPrimitiveType = explorer.GetTaggedValue(classElement, "MOFPrimitiveType");
			if (!mofPrimitiveType.IsNullOrEmpty())
			{
				result.SetCsharpType(mofPrimitiveType.ToLower());
			}
			else
			{
				result.SetCsharpType(classElement.Name.ToLower());
			}
			

			return result;
		}

		private bool TranslateContainment(string containment)
		{
			return containment != "Not Specified";
		}

		private int TranslateLowerBound(string bound)
		{
			return TranslateBound(bound, 0);
		}

		private int TranslateUpperBound(string bound)
		{
			return TranslateBound(bound, 1);
		}

		private int TranslateBound(string bound, int def)
		{
			if (bound == "")
				return def;
			else if (bound == "*")
				return -1;
			else
			{
				try
				{
					return Int32.Parse(bound);
				}
				catch (Exception)
				{
					return def;
				}
			}
		}

		private EMOF.IPrimitiveType ConstructPrimitiveType(string typeName)
		{
			switch (typeName.ToLower())
			{
				case "bool":
				case "boolean":
					return PrimitiveTypes.BOOLEAN;
				case "long":
				case "double":
					return PrimitiveTypes.REAL;
				case "int":
				case "short":
					return PrimitiveTypes.INTEGER;
				default:
					return PrimitiveTypes.STRING;
			}
		}

		public EMOF.IType ConstructTypeOfTyped(EnAr.Element typedElement)
		{
			EnAr.Element typeElement = explorer.GetClassElement(typedElement);
			if (typeElement != null)
				return ConstructType(typeElement);
			else
				return ConstructPrimitiveType(typedElement.Type);
		}

		public EMOF.IType ConstructTypeOfTyped(EnAr.Attribute typedAttribute)
		{
			EnAr.Element typeElement = explorer.GetClassElement(typedAttribute);
			if (typeElement != null)
				return ConstructType(typeElement);
			else
				return ConstructPrimitiveType(typedAttribute.Type);
		}

        public EMOF.IType ConstructTypeOfMethod(EnAr.Method method)
        {
            if (method.ReturnType == null)
                return null;
            EnAr.Element returnType = explorer.repository.GetElementByGuid(method.ReturnType);
            if (returnType != null)
                return ConstructType(returnType);
            else
                return ConstructPrimitiveType(method.ReturnType);
        }

        public EMOF.IType ConstructTypeOfParameter(EnAr.Parameter parameter)
        {
            if (parameter.Type == null)
                return null;
            EnAr.Element returnType = explorer.repository.GetElementByGuid(parameter.Type);
            if (returnType != null)
                return ConstructType(returnType);
            else
                return ConstructPrimitiveType(parameter.Type);
        }

        private EMOF.IType ConstructType(EnAr.Element classElement)
		{
			if (elementToType.ContainsKey(classElement))
			{
				return elementToType[classElement];
			}
			else
			{
				EMOF.IType result;
				// The type can be primitive
				if (classElement.Stereotype.ToLower() == "primitive")
				{
					// We create a PrimitiveType
					result = ConstructPrimitiveType(classElement);
					elementToType.Add(classElement, result);
				}
				// Or a class
				else
				{
					result = ConstructClass(classElement);
				}
				return result;
			}
		}

		private EMOF.IProperty ConstructProperty(EnAr.Attribute attribute)
		{
			EMOF.IProperty property = new EMOF.Property()
			{
				Name = attribute.Name,
				Type = ConstructTypeOfTyped(attribute),
				Default = attribute.Default,
				IsComposite = TranslateContainment(attribute.Containment),
				IsDerived = attribute.IsDerived,
				Lower = TranslateLowerBound(attribute.LowerBound),
				Upper = TranslateUpperBound(attribute.UpperBound),
				IsOrdered = attribute.IsOrdered,
				IsReadOnly = attribute.IsConst,
				IsUnique = !attribute.AllowDuplicates,
			};
			return property;
		}

		private EMOF.IProperty ConstructProperty(EMOF.IClass source, EMOF.IClass target, EnAr.ConnectorEnd sourceC, EnAr.ConnectorEnd targetC)
		{
			if (!string.IsNullOrWhiteSpace(targetC.Role))
			{
				EMOF.IProperty property = new EMOF.Property()
				{
					Name = targetC.Role,
					Type = target,
					IsComposite = sourceC.Aggregation > 0,
					Class = source,
					IsDerived = targetC.Derived,
					IsUnique = !targetC.AllowDuplicates,
					Lower = TranslateMultiplicityLower(targetC.Cardinality),
					Upper = TranslateMultiplicityUpper(targetC.Cardinality),
				};
				source.OwnedAttribute.Add(property);
				return property;
			}

			return null;
		}

		private int TranslateMultiplicityUpper(string cardinality)
		{
			if (string.IsNullOrWhiteSpace(cardinality))
				return -1;
			if (cardinality.Length == 1)
				return TranslateBound(cardinality, 1);
			else if (cardinality.Last() == '.')
				return -1;
			else
				return TranslateBound(cardinality.Last().ToString(), 1);
		}

		private int TranslateMultiplicityLower(string cardinality)
		{
			if (string.IsNullOrWhiteSpace(cardinality))
				return 0;
			if (cardinality == "*")
				return 0;
			else
				return TranslateBound(cardinality[0].ToString(), 0);
		}

		private EMOF.IClass ConstructClass(EnAr.Element classElement)
		{
			// We create a EMOF.Class
			EMOF.IClass clazz = new EMOF.Class() { IsAbstract = classElement.Abstract.ToLower() == "true", Name = classElement.Name };
			elementToType.Add(classElement, clazz);

			// We browse the attributes of the EMOF.Class element (~= ecore attributes)
			foreach (EnAr.Attribute attribute in explorer.GetAttributes(classElement))
			{
				EMOF.IProperty property = ConstructProperty(attribute);
				property.Class = clazz;
				clazz.OwnedAttribute.Add(property);
			}

			// We browse the connectors (~= ecore references + inheritance links)
			foreach (EnAr.Connector connector in explorer.GetConnectorsWithSource(classElement))
			{
				EnAr.Element targetElement = explorer.GetTargetElement(connector);
				if (targetElement.Type.ToLower() == "class")
				{
					EMOF.IClass targetType = ConstructType(targetElement) as EMOF.Class;

					if (targetType != null)
					{
						// Case super type
						if (connector.Type.ToLower() == "generalization")
						{
							clazz.SuperClass.Add(targetType);
						}
						// Case reference(s)
						else
						{
							EMOF.IProperty prop1 = ConstructProperty(clazz, targetType, connector.ClientEnd, connector.SupplierEnd);
							EMOF.IProperty prop2 = ConstructProperty(targetType, clazz, connector.SupplierEnd, connector.ClientEnd);
							if (prop1 != null && prop2 != null)
							{
								prop1.Opposite = prop2;
								prop2.Opposite = prop1;
							}
						}
					}
				}
			}

			// And finally the methods
			foreach (EnAr.Method method in explorer.GetMethods(classElement))
			{
				EMOF.IOperation operation = ConstructOperation(method);
				clazz.OwnedOperation.Add(operation);
				operation.Class = clazz;
			}

			return clazz;
		}

		private EMOF.IOperation ConstructOperation(EnAr.Method method)
		{
			//TODO type: is the classifierID the return type?
			EMOF.IOperation operation = new EMOF.Operation()
			{
				Name = method.Name,
				Lower = 0,
				Upper = method.ReturnIsArray ? 1 : -1,
				// type = type
			};

			foreach (EnAr.Parameter enArParameter in method.Parameters)
			{
				EMOF.IParameter parameter = ConstructParameter(enArParameter);
				parameter.Operation = operation;
				operation.OwnedParameter.Add(parameter);
			}

			return operation;
		}

		private EMOF.IParameter ConstructParameter(EnAr.Parameter enArParameter)
		{
			//TODO type
			EMOF.IParameter parameter = new EMOF.Parameter()
			{
				Name = enArParameter.Name,
			};
			return parameter;
		}

		private EMOF.IPackage ConstructPackage(EnAr.Package metamodelPackage)
		{
			if (elementToPackage.ContainsKey(metamodelPackage))
			{
				return elementToPackage[metamodelPackage];
			}
			else
			{
				// We create the Package
				EMOF.IPackage package = new EMOF.Package() { Name = metamodelPackage.Name };
				elementToPackage.Add(metamodelPackage, package);

				// We create its owned Types
				foreach (EnAr.Element classChild in explorer.GetChildrenElementsWithType(metamodelPackage, "class"))
				{
					EMOF.IType type = ConstructType(classChild);
					package.OwnedType.Add(type);
					type.Package = package;
				}

				// We create its nested Packages
				foreach (EnAr.Package metamodelChild in explorer.GetChildrenPackages(metamodelPackage))
				{
					EMOF.IPackage nestedPackage = ConstructPackage(metamodelChild);
					package.NestedPackage.Add(nestedPackage);
					nestedPackage.NestingPackage = package;
				}

				return package;
			}
		}

		public Tuple<EMOF.IPackage, string> ConstructMetamodel(string metamodelName)
		{
			List<EnAr.Package> metamodelPackages = explorer.FindPackagesWithStereotype("metamodel");
			EnAr.Package metamodelPackage = metamodelPackages.Single(p => p.Name == metamodelName);
			EMOF.IPackage emofMetamodel = ConstructPackage(metamodelPackage);
			string alias = null;
			// We store the alias, if any
			if (!string.IsNullOrWhiteSpace(metamodelPackage.Alias))
				alias = metamodelPackage.Alias;
			return new Tuple<EMOF.IPackage, string>(emofMetamodel, alias);
		}

		/// <summary>
		/// Find all metamodels of the EA file and import them.
		/// </summary>
		/// <returns> A Tuple with the metamodels, and the aliases of the metamodel.</returns>
		public Tuple<List<EMOF.IPackage>, Dictionary<string, EMOF.IPackage>> ConstructMetamodels()
		{
			List<EMOF.IPackage> metamodels = new List<EMOF.IPackage>();
			Dictionary<string, EMOF.IPackage> aliases = new Dictionary<string, EMOF.IPackage>();

			// We browse all package elements with stereotype "metamodel"
			List<EnAr.Package> metamodelPackages = explorer.FindPackagesWithStereotype("metamodel");
			foreach (EnAr.Package metamodelPackage in metamodelPackages)
			{
				EMOF.IPackage emofMetamodel = ConstructPackage(metamodelPackage);

				// We import into an EMOF Package

				metamodels.Add(emofMetamodel);

				// We store the alias, if any
				if (!string.IsNullOrWhiteSpace(metamodelPackage.Alias))
					aliases.Add(metamodelPackage.Alias, emofMetamodel);
			}
			return new Tuple<List<EMOF.IPackage>, Dictionary<string, EMOF.IPackage>>(metamodels, aliases);
		}

		/*   public Tuple<EMOF.IPackage,IEnumerable<string>> ConstructMetamodel(string metamodelName)
		  {
				List<EnAr.Package> metamodelPackages = explorer.FindPackagesWithStereotype("metamodel");

				return null;
		  }
*/

		public static string GetFQN(EMOF.IPackage package)
		{
			if (package.NestingPackage == null)
			{
				return package.Name;
			}
			else
			{
				return GetFQN(package.NestingPackage) + "." + package.Name;
			}
		}

	    public EMOF.IPackage GetEMOFPackage(EnAr.Package p)
	    {
	        return elementToPackage[p];
	    }

	  
	}
}