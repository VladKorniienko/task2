using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomList
{
    public class Program
    {
      

        static void Main(string[] args)
        {
            /*CustomList<int> list1 = new CustomList<int>();

           list1.Notify += DisplayMessage;

           list1.Add(6);
           list1.Add(688);
           list1.Add(786);
           list1.Add(645);
           list1.Add(56123);
           list1.Add(1321);
           */
            var list1 = new CustomList<int> { 5, 77, 22, 66, 33, 99 };
            var addService = new AddService<int>();
            var deleteService = new DeleteService<int>();
            list1.Added += addService.OnAdd;
            list1.Deleted += deleteService.OnDelete;
            list1.Add(5);
            list1.Remove(77);
            list1.Remove(99);

            Console.ReadKey();
        }
    }
}
