using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CafeinaXml
{
    /// <summary>
    /// Xml Information
    /// </summary>
    public class XmlInfo
    {
        /// <summary>
        /// File directory
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Filename
        /// </summary>
        public string File { get; set; }

        /// <summary>
        /// Xml root element (node)
        /// </summary>
        public string RootElement { get; set; }

        /// <summary>
        /// (Auto) Fullpath
        /// </summary>
        public string FullPath { get; set; }

        /// <summary>
        /// Initializes a Xml File Info
        /// </summary>
        /// <param name="path"></param>
        /// <param name="file"></param>
        public void Init(string path, string file)
        {
            Path = path;
            File = file;
            RootElement = "CafeinaXml";
            FullPath = path + file + ".xml";
        }
    }
}
