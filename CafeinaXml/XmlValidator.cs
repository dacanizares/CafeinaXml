using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using CafeinaXml.XReflection;
using System.Globalization;

[assembly: CLSCompliant(true)]
namespace CafeinaXml
{
    // TODO: IMPROVE VALIDATION EXCEPTIONS WITH ENTIITY REFLECTION
    /// <summary>
    /// Validates and prepare entities for Xml operations
    /// </summary>
    internal class XmlValidator
    {

        /// <summary>
        /// Validate insert conditions and prepares identity properties.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="properties"></param>
        /// <param name="document"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool PrepareToInsert(object entity, Type type, XDocument document)
        {
            var reflectedEntity = EntityReflection.Reflect(type);

            switch (reflectedEntity.Key)
            {
                case PropertyType.Identity:
                    ReflectedProperty idProperty = reflectedEntity.ReflectedProperties.
                                                    Where(p => p.PropertyType == PropertyType.Identity).FirstOrDefault();
                    // Get Identifier value
                    int idInsert = GetNextID(document, type, idProperty.PropertyInfo.Name);
                    // Set Identity value
                    idProperty.PropertyInfo.SetValue(entity, idInsert, null);
                    break;

                case PropertyType.Unique:
                    var props = reflectedEntity.ReflectedProperties.Where(p => p.PropertyType ==PropertyType.Unique).ToList();
                    foreach(var p in props)
                    {
                        string value = p.PropertyInfo.GetValue(entity, null).ToString();
                        if(Exists(document, type, p.PropertyInfo.Name, value))
                        {
                            return false;
                        }
                    }
                    break;
            }      
              
            return true;
        }

        /// <summary>
        /// Returns entity's XElement to update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="properties"></param>
        /// <param name="document"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static XElement GetElement(object entity,  Type type, XDocument document)
        {
            var reflectedEntity = EntityReflection.Reflect(type);
            var properties = new List<string>();
            var values = new List<string>();

            switch (reflectedEntity.Key)
            {
                case PropertyType.Identity:
                    var identity = reflectedEntity.ReflectedProperties
                        .FirstOrDefault(p => p.PropertyType == PropertyType.Identity).PropertyInfo;

                    string identityValue = identity.GetValue(entity, null).ToString();

                    properties.Add(identity.Name);
                    values.Add(identityValue);
                    break;

                case PropertyType.Unique:
                    var props = reflectedEntity.ReflectedProperties
                        .Where(p => p.PropertyType ==PropertyType.Unique).ToList();

                    foreach (var property in props)
                    {
                        var value = property.PropertyInfo.GetValue(entity, null).ToString();
                        properties.Add(property.PropertyInfo.Name);
                        values.Add(value);
                    }
                    break;
            }

            return GetElement(document, type, properties, values);
        }

        private static int GetNextID(XDocument document, Type type, string property)
        {
            var d = document.Descendants(type.Name);
            if (d == null)
                return 1;

            d = document.Descendants("Entity");
            if (d == null)
                return 1;

            return (from e in d
                    orderby int.Parse(e.Element(property).Value, CultureInfo.CurrentCulture) descending
                    select int.Parse(e.Element(property).Value, CultureInfo.CurrentCulture))
                    .FirstOrDefault() + 1;
        }

        private static XElement GetElement(XDocument document, Type type, List<string> properties, List<string> values)
        {
            var d = document.Descendants(type.Name);
            if (d == null)
                return null;

            d = document.Descendants("Entity");
            if (d == null)
                return null;

            for(int i = 0; i < properties.Count; i++)
            {
                d = d.Where(e => e.Element(properties[i]).Value
                                    .Equals(values[i]))
                                    .ToList();
            }

            return d.FirstOrDefault();
        }

        private static bool Exists(XDocument document, Type type, string property, string value)
        {
            var d = document.Descendants(type.Name);
            if (d == null)
                return false;

            d = document.Descendants("Entity");
            if (d == null)
                return false;

            return  (from e in d
                     where e.Element(property).Value == value
                     select e).Count() == 0 ? false : true;
        }
    }
}
