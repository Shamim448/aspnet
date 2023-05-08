using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generices
{
public class DiscountCalculator<TProduct> where TProduct : Product 
    {
        public float Calculator(TProduct product)
        {   
            return product.Price;
        }
    }
}