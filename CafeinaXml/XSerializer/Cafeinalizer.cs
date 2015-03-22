using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CafeinaXml.XReflection;
using System.Xml.Linq;
using CafeinaXml.XReflection.DataStructures;
using System.Reflection;
using CafeinaXml.XReflection.DataStructures.Reflection;
using System.Diagnostics;

namespace CafeinaXml.XSerializer
{
    internal class Cafeinalizer
    {
        public XElement Cafeinalize(object entity, string name)
        {
            ReflectedEntity reflectedEntity = EntityReflection.Reflect(entity.GetType());

            XElement xElement = new XElement(name);

            foreach (var reflectedProperty in reflectedEntity.ReflectedProperties)
            {                
                Structure structure = reflectedProperty.Structure;

                switch (structure.StructureType)
                {
                    case StructureType.Single:
                        xElement.SetElementValue(reflectedProperty.PropertyInfo.Name,
                                                 Casts.ToSave(entity, reflectedProperty));
                        break;

                    case StructureType.List:
                        xElement.Add(
                            CafeinaList(reflectedProperty.PropertyInfo.GetValue(entity, null),
                                        reflectedProperty));
                        break;

                    default:
                        xElement.Add(
                            Cafeinalize(reflectedProperty.PropertyInfo.GetValue(entity, null),
                                        reflectedProperty.PropertyInfo.Name));
                        break;
                }                     

            }

            return xElement;
        }

        private XElement CafeinaList(object obj, ReflectedProperty reflectedProperty)
        {
            XElement element = new XElement(reflectedProperty.PropertyInfo.Name);
            ListReflected listReflected = ListReflection.Reflect(obj.GetType());
            
            for (int i = 0; i < listReflected.Count(obj); i++)
            {
                if (listReflected.GenericTypeStructure.StructureType == StructureType.Single)
                {
                    element.Add(new XElement("Element", listReflected.ElementAt(obj, i)));
                }
                else if (listReflected.GenericTypeStructure.StructureType == StructureType.Entity)
                {
                    element.Add(
                            this.Cafeinalize(listReflected.ElementAt(obj, i), "Element")
                        );
                }
                else
                {
                    throw new CafeinaXmlException("Lists of lists unsupported on this version.");
                }
            }
            return element;
        }
    }
}


/***
public XElement Cafeinalize(object obj, string name)
        {
            ReflectedEntity reflectedEntity = EntityReflection.GetProperties(obj.GetType());

            XElement xElement = new XElement(name);

            foreach (var reflectedProperty in reflectedEntity.ReflectedProperties)
            {
                
                Structure structure = reflectedProperty.Structure;

                if (structure.StructureType == StructureType.Single)
                {
                    xElement.SetElementValue(reflectedProperty.PropertyInfo.Name,
                                            Casts.ToSave(obj, reflectedProperty));                    
                }
                else if (structure.StructureType == StructureType.List)
                {
                    Cafeinalist(reflectedProperty.PropertyInfo.GetValue(obj, null));                    
                }
                else
                {
                    xElement.Add(
                        Cafeinalize(reflectedProperty.PropertyInfo.GetValue(obj, null),
                                    reflectedProperty.PropertyInfo.Name));
                        
                }

            }

            return xElement;
        }*/