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
        #region custom-comparer
        public class CaseInsensetiveComparer : IComparer<string>
        {
            public int Compare(string? x, string? y) => 
                string.Compare(x, y, StringComparison.OrdinalIgnoreCase);            
        }
        #endregion
        public int OrderBywithCustomComperar()
        {
            #region orderby-custom-comparer
            string[] words = { "aPPLE", "AbAcUs", "bRaNcH", "BlUeBeRrY", "ClOvEr", "cHeRry" };
            var sortedWords = words.OrderBy(x => x, new CaseInsensetiveComparer());
            foreach (var word in sortedWords)
            {
                Console.WriteLine(word);
            }
            #endregion
            return 0;
        }
        public int OrderingReversal()
        {
            #region reverse
            string[] digits = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

            var reversedIDigits = (
                from digit in digits
                where digit[1] == 'i'
                select digit)
                .Reverse();

            Console.WriteLine("A backwards list of the digits with a second character of 'i':");
            foreach (var d in reversedIDigits)
            {
                Console.WriteLine(d);
            }
            #endregion
            return 0;
        }


    }
}
