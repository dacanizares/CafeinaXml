using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CafeinaXml.XReflection.DataStructures;

namespace CafeinaXml.XReflection
{
    internal class ReflectedEntity
    {
        //public Type Type { get; set; }
        //public Structure Structure { get; set;}

        public List<ReflectedProperty> ReflectedProperties { get; set; }
        
        public PropertyType Key { get; set; }

        public ReflectedEntity(List<ReflectedProperty> reflectedProperties, PropertyType key)
        {
            this.ReflectedProperties = reflectedProperties;
            this.Key = key;
        }
    }
}
