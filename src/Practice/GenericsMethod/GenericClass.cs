using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericsMethod
{
    public class GenericClass
    {
        public int Add<T>(T a, T b)
        {               
            dynamic v1 = a;
            dynamic v2 = b;
            return v1 + v2;
        }
        public int Sub<T>(T a, T b)
        {
            dynamic v1 = a;
            dynamic v2 = b;
            return v1 - v2;
        }
        public int Mul<T>(T a, T b)
        {
            dynamic v1 = a;
            dynamic v2 = b;
            return v1 * v2;
        }
        public int div<T>(T a, T b)
        {
            dynamic v1 = a;
            dynamic v2 = b;
            return v1 - v2;
        }
    }
}
