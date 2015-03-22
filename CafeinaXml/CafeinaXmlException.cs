using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CafeinaXml
{
    [SerializableAttribute] 
    public class CafeinaXmlException : Exception
    {
        public CafeinaXmlException() 
        { }

        public CafeinaXmlException(string message, Exception innerException)
            : base(message, innerException)
        { } 

        public CafeinaXmlException(string message)
            :base(message)
        { }

        protected CafeinaXmlException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
    }
}
