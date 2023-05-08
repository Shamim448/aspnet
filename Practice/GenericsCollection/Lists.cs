using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericsCollection
{
    public class Lists
    {
       static void Main()
        {
            List<string> li = new List<string>();
            li.Add("Shamim");
            li.Add("Saba");
            li.Add("Fatema");
            li.Add("Apple");
            for (int i = 0; i < li.Count; i++)
            {
                Console.WriteLine(li[i]);
            }
            //Capacity of link
            int t = li.Capacity;
            Console.WriteLine("Index Size : " + t); 
            //Add value into the middle
            li.Insert(3, "Israt");
            foreach (string s in li)
            {
                Console.WriteLine(s);
            }
            //Capacity of link after add another value
            int t1 = li.Capacity;
            Console.WriteLine("Number of Value : " + li.Count);
            Console.WriteLine("Index Size : " + t1);
            //Remove item using value and index
            li.Remove("Fatema");
            li.RemoveAt(2);
            foreach (string s in li)
            {
                Console.WriteLine(s);
            }

        }
    }
}
