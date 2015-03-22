using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CafeinaXml
{
    /// <summary>
    /// CafeinaXmlException
    /// </summary>
    [SerializableAttribute] 
    public class CafeinaXmlException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        public CafeinaXmlException() 
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public CafeinaXmlException(string message, Exception innerException)
            : base(message, innerException)
        { } 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public CafeinaXmlException(string message)
            :base(message)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected CafeinaXmlException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
    }
}
