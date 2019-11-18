using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomList
{
    public class AddService<T>
    {
        public void OnAdd(object source, ChangedArgs<T>  e)
        {
            Console.WriteLine($"Added value:{e.Item}");
        }
    }
}
