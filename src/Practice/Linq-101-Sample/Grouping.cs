using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq_101_Sample
{
    public class Grouping
    {
        public List<Product> GetProductList() => Products.ProductList;
        public List<Customer> GetCustomerLists() => Customers.CustomerList;
        public int GroupingSyntax()
        {
            #region groupby-syntex
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            var numberGroup = from n in numbers group n by n % 5  into g select (Reminder: g.Key, Numbers: g);
            foreach (var number in numberGroup)
            {
                Console.WriteLine($"Numbers  with a reminder of {number.Reminder} when divided by 5:");
                foreach(var v in number.Numbers)
                {
                    Console.WriteLine(v);
                }
            }
            #endregion
            return 0;
        }
        public int GroupByProperty()
        {
            #region groupby-property
            string[] words = { "blueberry", "chimpanzee", "abacus", "banana", "apple", "cheese" };
            var wordGroup = from w in words group w by w[0] into g select(FristLetter: g.Key, Words: g);
            foreach (var word in wordGroup)
            {
                Console.WriteLine(word.FristLetter);
                foreach(var w in word.Words)
                {
                    Console.WriteLine(w);
                }
            }
            #endregion
            return 0;
        }
        public int GroupByCategory()
        {
            #region groupby-category
            List<Product> products = GetProductList();
            var orderGroups = from p in products
                              group p by p.Category into g
                              select (Category: g.Key, Products: g);
            foreach (var orderGroup in orderGroups)
            {
                Console.WriteLine($"Products in {orderGroup.Category} category:");
                foreach (var product in orderGroup.Products)
                {
                    Console.WriteLine($"\t{product}");
                }
            }
            #endregion
            return 0;
        }
        public int NestedGroup()
        {
            #region nested-group
            List< Customer > Customer = GetCustomerLists();
            var orderGroups = from c in Customer select (CompanyName: c.CompanyName, 
                              YearlyOrder: from o in c.Orders group o by o.OrderDate.Year into yg
                                           select(Year:yg.Key, 
                                           MonthlyOrder: from o in yg group o by o.OrderDate.Month into mg
                                                         select(Month:mg.Key, Orders:mg)
                                                         )
                                           );
            foreach (var orderByCustome in orderGroups)
            {
                Console.WriteLine($"Customer Name: {orderByCustome.CompanyName}");
                foreach(var orderByYear in orderByCustome.YearlyOrder)
                {
                    Console.WriteLine($"\tYear: {orderByYear.Year}");
                    foreach(var orderByMonth in orderByYear.MonthlyOrder)
                    {
                        Console.WriteLine($"\t\tMonth:{orderByMonth.Month}");
                        foreach(var orders in orderByMonth.Orders)
                        {
                            Console.WriteLine($"\t\t\t\tOrder: {orders}");
                        }
                    }
                }
            }
            #endregion
            return 0;
        }


    }
}
