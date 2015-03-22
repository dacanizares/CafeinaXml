using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace CafeinaXml
{
    /// <summary>
    /// ExtendedXml methods
    /// </summary>
    public static class ExtendedXml
    {
        /// <summary>
        /// Returns a string value for a property
        /// </summary>
        /// <param name="element">Current XElement</param>
        /// <param name="propertyName">String containing the name or enum value</param>
        /// <returns>String value</returns>
        public static string Property(this XContainer element, Object propertyName)
        {
            if (element == null || propertyName == null)
                return string.Empty;
            return element.Element(propertyName.ToString()).Value;
        }
    }
}
