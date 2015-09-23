using CafeinaXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example
{
    class EntityDAO
    {
        const string FOLDER = "data/";
        const string FILENAME = "Entity";

        public MyEntity GetByID(int id)
        {
            using (var xml = new XmlDataContext(FOLDER, FILENAME))
            {
                return (from e in xml.GetElements<MyEntity>()
                        where e.Property(MyEntity.Props.ID) == id.ToString()
                        select xml.Load<MyEntity>(e)).FirstOrDefault();
            }
        }

        public List<MyEntity> GetAll()
        {
            using (var xml = new XmlDataContext(FOLDER, FILENAME))
            {
                return (from e in xml.GetElements<MyEntity>()
                        select xml.Load<MyEntity>(e)).ToList();
            }
        }

        public void Insert(MyEntity e)
        {
            using (var xml = new XmlDataContext(FOLDER, FILENAME))
            {
                xml.Insert(e);
            }
        }

        public void Update(MyEntity e)
        {
            using (var xml = new XmlDataContext(FOLDER, FILENAME))
            {
                xml.Update(e);
            }
        }

        public void Delete(MyEntity e)
        {
            using (var xml = new XmlDataContext(FOLDER, FILENAME))
            {
                xml.Delete(e);
            }
        }
    }
}
