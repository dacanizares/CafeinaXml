using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CafeinaXml.XReflection.DataStructures
{
    internal enum StructureType
    {
        Single, Entity, List
    }

    /// <summary>
    /// Represents the type structure, including generic type if it is necessary.
    /// </summary>
    internal class Structure
    {
        public StructureType StructureType { get; set; }

        public List<Type> GenericType { get; set;}

        public Structure(StructureType structureType)
        {
            this.StructureType = structureType;
        }

        public Structure(StructureType structureType, List<Type> genericType)
        {
            this.StructureType = structureType;
            this.GenericType = genericType;
        }
    }
}
