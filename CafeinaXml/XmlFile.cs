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
        /// File directory
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Filename
        /// </summary>
        public string Filename { get; set; }

        /// <summary>
        /// Xml root element (node)
        /// </summary>
        public string RootElement { get; set; }

        /// <summary>
        /// (Auto) Fullpath
        /// </summary>
        public string FullPath { get; set; }

        public void Init(string path, string file)
        {
            Path = path;
            Filename = file;
            RootElement = "CafeinaXml";
            FullPath = path + file + ".xml";
        }

        /// <summary>
        /// Opens a Xml file
        /// </summary>
        /// <param name="info">File info</param>
        /// <returns>XDocument</returns>
        public static XDocument Open(XmlInfo info)
        {
            //Verify if file exists
            if (false == File.Exists(info.FullPath))
            {
                Create(info.Path, info.FullPath);
            }

            return XDocument.Load(info.FullPath);
        }

        /// <summary>
        /// Creates an Xml file
        /// </summary>
        /// <param name="path"></param>
        /// <param name="fullpath"></param>
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

        private XmlFile()
        { }
    }
}
