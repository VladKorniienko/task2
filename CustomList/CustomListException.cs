using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomList
{
    class CustomListException : ArgumentException
    {
        public int Value1 { get; }
      
        public CustomListException(string message)
            : base(message)
        {

        }
        public CustomListException(string message, int value1)
            : base(message)
        {
            Value1 = value1;
        }
        
    }
}
