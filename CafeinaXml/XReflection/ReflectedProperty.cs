using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using CafeinaXml.XReflection.DataStructures;

namespace CafeinaXml.XReflection
{
    internal class ReflectedProperty
    {
        public PropertyInfo PropertyInfo { get; set; }
        public PropertyType PropertyType { get; set; }
        public Structure Structure { get; set; }

        public ReflectedProperty(PropertyInfo propertyInfo, Structure structure)
        {
            this.PropertyInfo = propertyInfo;
            this.PropertyType = PropertyType.None;
            this.Structure = structure;
        }
    }
}
