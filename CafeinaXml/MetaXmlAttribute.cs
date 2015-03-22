using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CafeinaXml
{    
    /// <summary>
    /// Key types
    /// NOTE: Do not use KeyType.None in your entity attributes. 
    /// </summary>
    public enum PropertyType
    {
        None, Identity, Unique/*, Required*/
    }

    /// <summary>
    /// Define an special type of property
    /// </summary>

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class MetaXmlAttribute : Attribute
    {
        private PropertyType _PropertyType;

        public PropertyType PropertyType
        {
            get { return _PropertyType; }
        }

        /// <summary>
        /// Sets a default Key with KeyType.Identity
        /// </summary>
        public MetaXmlAttribute()
        {
            _PropertyType = PropertyType.Identity;
        }

        /// <summary>
        /// Sets a Key
        /// </summary>
        /// <param name="keyType">KeyType</param>
        public MetaXmlAttribute(PropertyType propertyType)
        {
            if (propertyType == PropertyType.None)
                throw new CafeinaXmlException("None Property Type is invalid in this context. Set another Property Type");

            _PropertyType = propertyType;
        }
    }
}
