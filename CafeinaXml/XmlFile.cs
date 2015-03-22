using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;

namespace CafeinaXml
{
    internal class XmlFile
    {
        /// <summary>
        /// Xml root element
        /// </summary>
        public string ROOT_ELEMENT = "CafeinaXml";
        
        /// <summary>
        /// File directory
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Filename
        /// </summary>
        public string Filename { get; set; }
        
        /// <summary>
        /// (Auto) Fullpath
        /// </summary>
        public string FullPath { get; set; }

        public void Init(string path, string file)
        {
            Path = path;
            Filename = file;
            FullPath = path + file + ".xml";
        }

        /// <summary>
        /// Opens a Xml file
        /// </summary>
        /// <param name="info">File info</param>
        /// <returns>XDocument</returns>
        public XDocument Open()
        {
            //Verify if file exists
            if (false == File.Exists(this.FullPath))
            {
                Create(this.Path, this.FullPath);
            }

            return XDocument.Load(this.FullPath);
        }

        /// <summary>
        /// Creates an Xml file
        /// </summary>
        /// <param name="path"></param>
        /// <param name="file"></param>
        /// <param name="fullpath"></param>
        /// <param name="rootElement"></param>
        private static void Create(string path, string fullpath)
        {
            //Create files and directories
            if (false == Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            XDocument xmlDoc =
                new XDocument(
                    new XDeclaration("1.0", "UTF-8", "yes"),
                    new XElement("CafeinaXml", new XAttribute("version", "CFXML1.0"))
                );
            xmlDoc.Save(fullpath);
        }

        public XmlFile()
        { }
    }
}
