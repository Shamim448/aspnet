using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq_101_Sample
{
    public class Ordering
    {
        public List<Product> GetProductList() => Products.ProductList;
        public List<Customer> GetCustomerLists() => Customers.CustomerList;

        public int OrderbySyntax()
        {
            #region orderby-syntax
            string[] words = { "cherry", "apple", "blueberry" };

            var sortedWords = from word in words 
                              orderby word 
                              select word;

            Console.WriteLine("The sorted list of words:");
            foreach (var w in sortedWords)
            {
                Console.WriteLine(w);
            }
            #endregion
            return 0;
        }
        public int OrderbyProperty()
        {
            #region orderby-property
            string[] words = { "cherry", "apple", "blueberry" };

            var sortedWords = from word in words
                              orderby word.Length
                              select word;

            Console.WriteLine("The sorted list of words (by length):");
            foreach (var w in sortedWords)
            {
                Console.WriteLine(w);
            }
            #endregion
            return 0;
        }
        public int OrderByProducts()
        {
            #region orderby-user-types
            List<Product> products = GetProductList();

            var sortedProducts = from prod in products
                                 orderby prod.ProductName
                                 select prod;

            foreach (var product in sortedProducts)
            {
                Console.WriteLine(product);
            }
            #endregion
            return 0;
        }
        public int ThenBySyntax()
        {
            #region thenby-syntax
            string[] digits = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

            var sortedDigits = from digit in digits orderby digit.Length, digit select digit;

            Console.WriteLine("Sorted digits:");
            foreach (var d in sortedDigits)
            {
                Console.WriteLine(d);
            }
            #endregion
            return 0;
        }


    }
}
