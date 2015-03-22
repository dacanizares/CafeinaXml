using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using CafeinaXml.XReflection;
using CafeinaXml.XReflection.DataStructures.Reflection;
using CafeinaXml.XReflection.DataStructures;
using System.Reflection;

namespace CafeinaXml.XSerializer
{
    // TODO: Remove <T>
    class Decafeinalizer<T> where T : new()
    {
        public T Decafeinalize(XElement element)
        {
            T entity = new T();

            ReflectedEntity reflectedEntity = EntityReflection.Reflect(entity.GetType());

            foreach (var reflectedProperty in reflectedEntity.ReflectedProperties)
            {
                if (element.Element(reflectedProperty.PropertyInfo.Name) == null)
                    continue;

                Structure structure = reflectedProperty.Structure;

                switch (structure.StructureType)
                {
                    case StructureType.Single:
                        object obj = element.Element(reflectedProperty.PropertyInfo.Name).Value;
                        PropertyInfo propertyInfo = reflectedProperty.PropertyInfo;
                        propertyInfo.SetValue(entity, Casts.ToLoad(obj, propertyInfo.PropertyType), null);
                        break;

                    case StructureType.List:
                        obj = DecafeinaList(element.Element(reflectedProperty.PropertyInfo.Name), reflectedProperty);
                        propertyInfo = reflectedProperty.PropertyInfo;
                        propertyInfo.SetValue(entity, obj, null);
                        break;

                    default:
                        obj = GenericsReflection.GenericCall(typeof(Decafeinalizer<>),
                            new Type[] { reflectedProperty.PropertyInfo.PropertyType }, this.GetType().Name,
                            new object[] { element.Element(reflectedProperty.PropertyInfo.Name) });
                        propertyInfo = reflectedProperty.PropertyInfo;
                        propertyInfo.SetValue(entity, obj, null);
                        break;
                }
            }
            return entity;
        }



        private static object DecafeinaList(XElement element, ReflectedProperty reflectedProperty)
        {
            ListReflected listReflected = ListReflection.Reflect(reflectedProperty.PropertyInfo.PropertyType);

            object list = listReflected.NewInstance();

            foreach (var e in element.Descendants("Element").ToList())
            {
                if (listReflected.GenericTypeStructure.StructureType == StructureType.Single)
                {
                    listReflected.Add(list, Casts.ToLoad(e.Value, listReflected.GenericType));
                }
                else if (listReflected.GenericTypeStructure.StructureType == StructureType.Entity)
                {
                    listReflected.Add(list,
                        GenericsReflection.GenericCall(typeof(Decafeinalizer<>),
                            new Type[] { listReflected.GenericType }, "Decafeinalize",
                            new object[] { e })
                            );
                }
                else
                {
                    throw new CafeinaXmlException("Lists of lists unsupported on this version.");
                }
            }
            return list;
        }
    }
}
