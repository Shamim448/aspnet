using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq_101_Sample
{
    public class AggregateOperators
    {
        public List<Product> GetProductList() => Products.ProductList;
        public List<Customer> GetCustomerList() => Customers.CustomerList;
        public int CountSyntax()
        {
            #region count-syntax
            int[] factorsOf300 = { 2, 2, 3, 5, 5 };
            int uniqueFactors = factorsOf300.Distinct().Count();
            Console.WriteLine($"There are {uniqueFactors} unique factors of 300.");
            #endregion
            return 0;
        }
        public int NestedCount()
        {
            #region nested-count
            List<Customer> customers = GetCustomerList();
            var q = from c in customers select (c.CustomerID, OrderCount: c.Orders.Count());
            foreach (var customer in q)
            {
                Console.WriteLine($"ID : {customer.CustomerID}, count: {customer.OrderCount}");
            }
            #endregion
            return 0;
        }
        public int GroupedCount()
        {
            #region grouped-count
            List<Product> products = GetProductList();
            var q = from p in products group p by p.Category into g
                    select(Category:g.Key, ProductCount: g.Count());
            foreach (var product in q)
            {
                Console.WriteLine($"Category: {product.Category}: Product Count: {product.ProductCount}");
            }
            #endregion
            return 0;
        }
        public int SumGrouped() {
            #region grouped-sum
            List<Product> products = GetProductList();
            var q = from p in products group p by p.Category into g
                    select(Category: g.Key, TotalUnitsInStock: g.Sum(p => p.UnitsInStock)); 
            foreach (var product in q)
            {
                Console.WriteLine($"Category: {product.Category}, Units in stock: {product.TotalUnitsInStock}");
            }
            #endregion
            return 0;
        }


    }
}
