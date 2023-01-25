using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericsMethod
{
    public class GenericClass<T>
    {
        public T Add(T a, T b)
        {               
            dynamic v1 = a;
            dynamic v2 = b;
            return v1 + v2;
        }
        public T Sub(T a, T b)
        {
            dynamic v1 = a;
            dynamic v2 = b;
            return v1 - v2;
        }
        public T Mul(T a, T b)
        {
            dynamic v1 = a;
            dynamic v2 = b;
            return v1 * v2;
        }
        public T Div(T a, T b)
        {
            dynamic v1 = a;
            dynamic v2 = b;
            return v1 / v2;
        }
    }
}
