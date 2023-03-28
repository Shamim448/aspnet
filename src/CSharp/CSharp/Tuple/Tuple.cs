using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tuple
{
    public class Tuples
    {
        public (int, int ) Calculation(int x, int y)
        {
            int sum = x + y;
            int multiplication = x * y;

            return (sum, multiplication);
        }
    }
}
