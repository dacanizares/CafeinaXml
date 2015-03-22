using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CafeinaXml.XReflection.DataStructures.Reflection
{
    internal class ListReflection
    {
        private static Dictionary<Type, ListReflected> Precached = new Dictionary<Type,ListReflected>();
        
        public static ListReflected Reflect(Type type)
        {
            if (Precached.ContainsKey(type))
            {
                return Precached[type];
            }

            ListReflected listReflected = new ListReflected(
                type.GetProperty("Count").GetGetMethod(),
                type.GetProperty("Item").GetGetMethod(),
                type.GetMethod("Add")
                );

            listReflected.Structure = StructureClassifier.GetStructure(type);
            listReflected.GenericType = listReflected.Structure.GenericType[0];
            listReflected.GenericTypeStructure = StructureClassifier.GetStructure(listReflected.GenericType);

            Precached.Add(type, listReflected);

            return listReflected;
        }

        private ListReflection() 
        { }
    }
}
