using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generices
{
    public class Employee<T>
    {
        public string Name { get; set; }
        public T Type { get; set; }
        public void print<T>(Employee<T> value)
        { 
            Console.WriteLine("Name: " + value.Name + "Type: " + value.Type);
        }
    }
    
}
