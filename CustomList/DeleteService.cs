using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomList
{
    public class DeleteService<T>
    {
        public void OnDelete(object source, ChangedArgs<T> e)
        {
            Console.WriteLine($"Deleted value:{e.Item}");
        }
    }
}
