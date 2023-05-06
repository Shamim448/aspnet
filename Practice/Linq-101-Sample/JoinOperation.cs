using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq_101_Sample
{
    public class JoinOperation
    {
        public List<Product> GetProductList() => Products.ProductList;
        public List<Customer> GetCustomerLists() => Customers.CustomerList;
        public int CrossJoinQuery()
        {
            #region cross-join
            string[] categories = { "Beverages", "Condiments", "Vegetables", "Dairy Products", "Seafood" };
            List<Product> products = GetProductList();
            var q = from c in categories
                    join p in products on c equals p.Category
                    select (Category: c, pName: p.ProductName);
            foreach (var Name in q)
            {
                Console.WriteLine(Name.pName + " - " + Name.Category);
            }
            #endregion
            return 0;
        }
        public int GroupJoinQquery()
        {
            #region group-join
            string[] categories = {
                "Beverages",
                "Condiments",
                "Vegetables",
                "Dairy Products",
                "Seafood"
            };
            List<Product> products = GetProductList();
            var q = from c in categories join p in products on c equals p.Category into ps select(Category : c, Products: ps);
            foreach (var catagory in q)
            {
                Console.WriteLine(catagory.Category + ":" );
                foreach(var p in catagory.Products)
                {
                    Console.WriteLine("\t" + p.ProductName);
                }
            }
            #endregion
            return 0;
        }
        public int CrossGroupJoin()
        {
            #region Cross-Group-Join
            string[] categories = {
                "Beverages",
                "Condiments",
                "Vegetables",
                "Dairy Products",
                "Seafood"
            };
            List<Product> products = GetProductList();
            var q = from c in categories
                    join p in products on c equals p.Category into ps
                    from p in ps
                    select (Category: c, p.ProductName);

            foreach (var v in q)
            {
                Console.WriteLine(v.ProductName + ": " + v.Category);
            }
            #endregion
            return 0;

        }
        public int LeftOuterJoin()
        {
            #region left-outer-join
            string[] categories = {
                "Beverages",
                "Condiments",
                "Vegetables",
                "Dairy Products",
                "Seafood"
            };
            List<Product> products = GetProductList();
            var q = from c in categories join p in products on c equals p.Category into ps
                    from p in ps.DefaultIfEmpty() select(Category: c, ProductName: p == null ? "(No Product)" : p.ProductName);
            foreach (var v in q)
            {
                Console.WriteLine($"{v.ProductName} : {v.Category}");
            }
            #endregion
            return 0;
        }
        }
    
};
