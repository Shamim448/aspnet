using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace param
{
    public class pam
    {
        public void pRef(ref int a)
        {
            a = 10;
            Console.WriteLine(a);
        }
        public void pIn(in int a)
        {
         
            Console.WriteLine(a);
        }
        public void pOut(out int a)
        {
            a = 20;
            Console.WriteLine(a);
        }

    }
}
