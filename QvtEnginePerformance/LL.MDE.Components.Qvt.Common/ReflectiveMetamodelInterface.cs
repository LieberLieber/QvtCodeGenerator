using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace LL.MDE.Components.Qvt.Common
{
    public class ReflectiveMetamodelInterface : IMetaModelInterface
    {
        private static object CommonAddOrSet(object element, string fieldName, object newValue = null)
        {
            // We get the type of the element
            Type type = element.GetType();

            // We get the member named "collectionName"
            MemberInfo prop = type.GetMember(fieldName).Single();

            // Depending whether this is a 'property' (ie. with explicit get/set methods)
            // or a 'field', we prepare the get/set flags and we get the type of the member
            Type memberType;
            BindingFlags setFlag;
            BindingFlags getFlag;
            if (prop is PropertyInfo)
            {
                setFlag = BindingFlags.SetProperty;
                getFlag = BindingFlags.GetProperty;
                memberType = ((PropertyInfo)prop).PropertyType;
            }
            else if (prop is FieldInfo)
            {
                setFlag = BindingFlags.SetField;
                getFlag = BindingFlags.GetField;
                memberType = ((FieldInfo)prop).FieldType;
            }
            else
            {
                throw new Exception("ReflectiveMetamodelInterface cannot manage members of type: " + prop);
            }

            // We check if the member type is a subtype of ICollection
            bool isCollection = typeof(ICollection).IsAssignableFrom(memberType);

            // If it is a collection
            if (isCollection)
            {
                // We get the value of the generic type of the collection
                Type collectionContentType = memberType.GetGenericArguments().Single();

                // We try to retrieve an existing collection in the element
                ICollection collection = (ICollection)type.InvokeMember(fieldName, getFlag, null, element, null);

                // If there is no collection yet, we have to create it
                if (collection == null)
                {
                    // We get the default constructor of the collection type
                    ConstructorInfo[] constructors = memberType.GetConstructors();
                    ConstructorInfo constructor = constructors.Single(c => c.GetParameters().Length == 0);

                    // And we use it to create the collection object
                    object[] noArgs = { };
                    object newCollection = constructor.Invoke(noArgs);
                    collection = (ICollection)newCollection;

                    // We assign the collection object to the member "collectionName"
                    object[] collectionAsArray = { collection };
                    type.InvokeMember(fieldName, setFlag, null, element, collectionAsArray);
                }

                // We create a new instance of this generic type
                // /!\ WARNING: 'GetUninitializedObject' does not rely on any construtor,
                // so it creates a completely blank object and bypasses ALL defined constructors!
                if (newValue == null)
                    newValue = FormatterServices.GetUninitializedObject(collectionContentType);

                // We find the add operation of the collection type
                MethodInfo addMethod = memberType.GetMethod("Add");

                // And we add the new value to the (potentially new) collection
                object[] newValueAsArray = { newValue };
                addMethod.Invoke(collection, newValueAsArray);
                return newValue;
            }

            // If it isn't a collection
            else
            {
                // We create a new instance of the member type
                // /!\ WARNING: 'GetUninitializedObject' does not rely on any construtor,
                // so it creates a completely blank object!
                if (newValue == null)
                    newValue = FormatterServices.GetUninitializedObject(memberType);

                // And we assign the new instance to the member "collectionName"
                object[] newValueAsArray = { newValue };
                type.InvokeMember(fieldName, setFlag, null, element, newValueAsArray);
                return newValue;
            }
        }

        public void AddOrSetInField(object element, string fieldName, object value)
        {
            CommonAddOrSet(element, fieldName, value);
        }

        public object CreateNewObjectInField(object element, string fieldName)
        {
            return CommonAddOrSet(element, fieldName);
        }
    }
}