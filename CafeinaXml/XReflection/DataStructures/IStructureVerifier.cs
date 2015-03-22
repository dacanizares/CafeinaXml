using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CafeinaXml.XReflection.DataStructures
{
    interface IStructureVerifier
    {
        bool Verify(Type type);

        Structure GetType(Type type);
    }
}
