using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CafeinaXml.XReflection.DataStructures.Structures
{
    internal class SingleStructure : IStructureVerifier
    {
        private List<Type> singleTypes = new List<Type>()
        {
            typeof(string),
            typeof(bool), 
            typeof(byte),
            typeof(char),
            typeof(decimal),
            typeof(double),
            typeof(float),
            typeof(int),
            typeof(long),
            typeof(sbyte),
            typeof(short),
            typeof(uint),
            typeof(ulong),
            typeof(ushort)            
        };
 
        
        public bool Verify(Type type)
        {
            return type.IsEnum || (singleTypes.Contains(type));
        }

        public Structure GetType(Type type)
        {
            return new Structure(StructureType.Single);
        }
    }
}
