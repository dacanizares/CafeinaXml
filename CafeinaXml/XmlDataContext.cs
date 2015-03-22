using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Reflection;
using System.IO;
using CafeinaXml.XReflection;
using CafeinaXml.XSerializer;

namespace CafeinaXml
{
    // THIS CLASS EXTENDS IDisposable TO IMPROVE MULTIUSER IMPLEMENTATION (NEXT RELEASE OF CAFEINA-XML)
    // IT AUTOMATICALLY WILL RELASE AND BLOCK ALL RESOURCES THAT IT NEEDS

    /// <summary>
    /// This class is the main entry-point of all operations
    /// </summary>
    public class XmlDataContext : IDisposable
    {
        private XmlFile XmlFile;

        private XDocument Document;
        
        /// <summary>
        /// Get all Xml elements related to the T entity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IEnumerable<XElement> GetElements<T>()
        {
            var entity = typeof(T).Name.ToString();
            
            return Document.Descendants(entity).Descendants("Entity");
        }
       
        /// <summary>
        /// Loads a XElement as a T entity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="e"></param>
        /// <returns></returns>
        public T Load<T>(XElement element) where T : new()
        {
            if (element == null)
                throw new CafeinaXmlException("element can not be null in order to load it");
            Decafeinalizer<T> decafeinalizer = new Decafeinalizer<T>();
            return decafeinalizer.Decafeinalize(element);
        }

        /// <summary>
        /// Loads all T entities
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="e"></param>
        /// <returns></returns>
        public List<T> Load<T>() where T : new()
        {
            Decafeinalizer<T> decafeinalizer = new Decafeinalizer<T>();
            List<T> entities = new List<T>();
            foreach (var element in this.GetElements<T>())
            {
                entities.Add(decafeinalizer.Decafeinalize(element));
            }
            return entities;
        }

        /// <summary>
        /// Saves an Xml entity without key validations
        /// </summary>
        /// <param name="entity"></param>
        public void Save(object entity)
        {
            if (entity == null)
                throw new CafeinaXmlException("You cannot save a null entity");

            Type type = entity.GetType();

            // Get Xml element ROOT
            XElement xmlElement = Document.Element(this.XmlFile.ROOT_ELEMENT);  
                                   
            // Add element
            if (xmlElement.Element(type.Name) == null)
            {
                XElement e = new XElement(type.Name);
                xmlElement.Add(e);                    
            }

            Cafeinalizer cafeinalizer = new Cafeinalizer();

            xmlElement.Element(type.Name).Add(cafeinalizer.Cafeinalize(entity, "Entity"));                

            // Save
            Document.Save(this.XmlFile.FullPath);
           
        }

        /// <summary>
        /// Inserts an Xml entity
        /// </summary>
        /// <param name="entity"></param>
        public void Insert(object entity)
        {
            if (entity == null)
                throw new CafeinaXmlException("You cannot insert a null entity");

            Type type = entity.GetType();

            // Validate and prepare for insert        
            if (!XmlValidator.PrepareToInsert(entity, type, Document))
            {
                throw new CafeinaXmlException(string.Concat("Error when trying to insert entity of type ", type.Name, ". Duplicated key."));
            }

            this.Save(entity);
            
        }

        /// <summary>
        /// Updates Xml entity
        /// </summary>
        /// <param name="entity"></param>
        public void Update(object entity)
        {
            if (entity == null)
                throw new CafeinaXmlException("You cannot delete a null entity");

            Type type = entity.GetType();

            //Search element
            XElement element = XmlValidator.GetElement(entity, type, Document);

            if (element == null)
            {
                throw new CafeinaXmlException("Error Updating. Element not found.");
            }

            // Remove, update and save
            element.Remove();
            this.Save(entity);
           
        }

        /// <summary>
        /// Delete Xml entity
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(object entity)
        {
            if (entity == null)
                throw new CafeinaXmlException("You cannot delete a null entity");

            Type type = entity.GetType();

            //Search element
            XElement element = XmlValidator.GetElement(entity, type, Document);
            
            if (element == null)
            {
                throw new CafeinaXmlException("Error deleting. Element not found.");
            }

            // Remove and save
            element.Remove();
            Document.Save(this.XmlFile.FullPath);
        }

        /// <summary>
        /// Deletes all entities of sended type
        /// </summary>
        /// <param name="type"></param>
        public void DeleteAll(Type type)
        {
            // Get Xml element ROOT
            XElement xmlElement = Document.Element(this.XmlFile.ROOT_ELEMENT);  
                                   
            if (xmlElement.Element(type.Name) != null)
            {
                xmlElement.Element(type.Name).Remove();
            }

            Document.Save(this.XmlFile.FullPath);
        }

        /// <summary>
        /// Starts a Xml datacontext for client enviroments
        /// </summary>
        /// <param name="path">Path to the Xml file. Example: "Data/XmlFiles/"</param>
        /// <param name="file">Xml file. Example: "Users" (.xml not required)</param>
        public XmlDataContext(string path, string file)
        {
            this.XmlFile = new XmlFile();
            this.XmlFile.Init(path, file);

            Document = XmlFile.Open();
        }

        /// <summary>
        /// Starts a Xml datacontext for sever enviroments
        /// </summary>
        /// <param name="path">Path to the Xml file. Exaple: "Data/XmlFiles/"</param>
        /// <param name="file">Xml file. Exmaple: "Users" (.xml not required)</param>
        /// <param name="appPath">Web application path. PLEASE SEND -AppDomain.CurrentDomain.BaseDirectory- FROM YOUR MAIN PROJECT</param>
        public XmlDataContext(string path, string file, string appPath)
        {
            this.XmlFile = new XmlFile();
            this.XmlFile.Init(appPath + path, file);

            Document = XmlFile.Open();
        }

        public void Dispose()
        {  }
    }
}
