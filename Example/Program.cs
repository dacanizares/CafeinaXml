using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            var fullname = Console.ReadLine();
            var website = Console.ReadLine();
            var email = Console.ReadLine();

            MyEntity e = new MyEntity() 
            { 
                FullName = fullname,
                Website = website,
                Email = email
            };

            EntityDAO dao = new EntityDAO();
            dao.Insert(e);

            foreach (var item in dao.GetAll()) 
            {
                Console.WriteLine(item.FullName);
            }
        }
    }
}
