using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CafeinaXml.XReflection.DataStructures.Structures
{
    class ListStructure : IStructureVerifier
    {
        public bool Verify(Type type)
        {
            foreach (Type interfaceType in type.GetInterfaces())
            {
                if (interfaceType.IsGenericType &&
                    interfaceType.GetGenericTypeDefinition() == typeof(IList<>))
                {
                    Type itemType = type.GetGenericArguments()[0];
                   
                    return true;
                }
            }
            return false;
        }

        public Structure GetType(Type type)
        {
            return new Structure(StructureType.List, new List<Type>(type.GetGenericArguments()));
        }
    }
}
