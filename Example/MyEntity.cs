using CafeinaXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example
{
    class MyEntity
    {
        public enum Props { ID, FullName }

        [MetaXml(PropertyType.Identity)]
        public int ID { get; set; }
        public string FullName { get; set; }
        public string Website { get; set; }
        public string Email { get; set; }

        public MyEntity() { }
    }
}
