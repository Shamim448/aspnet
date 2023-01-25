using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericsMethod
{
    public class GMethod
    {
        public bool commpire<T>(T a, T b)
        {
            if(a.Equals(b)) return true;
            return false;
        }
    }
}
