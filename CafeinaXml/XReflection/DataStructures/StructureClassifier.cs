using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CafeinaXml.XReflection.DataStructures.Structures;

namespace CafeinaXml.XReflection.DataStructures
{
    internal class StructureClassifier
    {
        // Precached structures
        static Dictionary<Type, Structure> Precached = new Dictionary<Type, Structure>();
        
        /// <summary>
        /// Registered verifiers
        /// </summary>
        private static List<IStructureVerifier> Verifiers = new List<IStructureVerifier>()
        {
           new SingleStructure(), new ListStructure()
        };

        /// <summary>
        /// Gets the structure type for a type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Structure GetStructure(Type type)
        {
            // Search in cache
            if (Precached.ContainsKey(type))
            {
                return Precached[type];
            }

            Structure structureType = new Structure(StructureType.Entity);

            foreach (var verifier in Verifiers)
            {
                if (verifier.Verify(type))
                {
                    structureType = verifier.GetType(type);
                    break;
                }
            }

            // Add to cache and return
            Precached.Add(type, structureType);
            return structureType;
        }

        private StructureClassifier()
        { }
    }
}
