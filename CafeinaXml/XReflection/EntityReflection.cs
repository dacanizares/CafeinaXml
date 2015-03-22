using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using CafeinaXml.XReflection.DataStructures;

namespace CafeinaXml.XReflection
{
    internal class EntityReflection
    {
        // Precached reflections
        static Dictionary<Type, ReflectedEntity> Precached = new Dictionary<Type, ReflectedEntity>();
        
        /// <summary>
        /// Reflection
        /// </summary>
        /// <param name="type">Data type to reflect</param>
        /// <returns>Full Reflected entity</returns>
        public static ReflectedEntity Reflect(Type type)
        {
            // Search in cache
            if (Precached.ContainsKey(type))
            {
                return Precached[type];
            }

            // Get properties
            PropertyInfo[] properties = type.GetProperties();

            var reflectedProperties = new List<ReflectedProperty>();

            foreach(var property in properties)
            {
                // Set default info
                var reflectedProperty = new ReflectedProperty(
                            property, StructureClassifier.GetStructure(property.PropertyType));
                
                // Search for key atributtes
                Attribute[] attrs = Attribute.GetCustomAttributes(property);                                  
                foreach (var attr in attrs)
	            {
                    var meta = attr as MetaXmlAttribute;
                    if (meta != null)
                    {                       
                        reflectedProperty.PropertyType = meta.PropertyType;                        
                    }
	            }

                reflectedProperties.Add(reflectedProperty);
            }
            
            // Add to cache and return
            ReflectedEntity reflectedEntity = new ReflectedEntity(reflectedProperties, ValidateKey(reflectedProperties));
            Precached.Add(type, reflectedEntity);
            return reflectedEntity;
        }

        /// <summary>
        /// Validates a reflections
        /// </summary>
        /// <param name="reflectedEntity"></param>
        public static PropertyType ValidateKey(List<ReflectedProperty> reflectedProperties)
        {
            int identityProps = reflectedProperties.Where(p => p.PropertyType == PropertyType.Identity).Count();

            int uniqueProps = reflectedProperties.Where(p => p.PropertyType == PropertyType.Unique).Count();

            // Errors
            if (identityProps > 1)
            {
                throw new CafeinaXmlException("A entity can't have more than one identity.");
            }
            if (identityProps > 0 && uniqueProps > 0)
            {
                throw new CafeinaXmlException("Only can exist one key attribute for identity-entities.");
            }
            if (identityProps == 1)
            {
                var id = reflectedProperties.Where(p => p.PropertyType == PropertyType.Identity).FirstOrDefault();
                if (id.Structure.StructureType != StructureType.Single)
                {
                    throw new CafeinaXmlException("Only single types allowed to use identity property type.");
                }
            }
            if (uniqueProps > 0)
            {
                foreach (var p in reflectedProperties.Where(p => p.PropertyType == PropertyType.Unique).ToList())
                {
                    if (p.Structure.StructureType != StructureType.Single)
                    {
                        throw new CafeinaXmlException("Only single types allowed to use unique property type.");
                    }
                }
            }

            // Results
            if(identityProps == 1)
            {
                return PropertyType.Identity;
            }
            if (uniqueProps > 0)
            {
                return PropertyType.Unique;
            }

            return PropertyType.None;
        }

        private EntityReflection()
        { }
         
    }
}
