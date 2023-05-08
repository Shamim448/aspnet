using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupJoin
{
    public class Employee
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int DepartmentID { get; set; }

        public static List<Employee> GetAllEmployees()
        {
            return new List<Employee>()
        {
            new Employee { ID = 1, Name = "Mark", DepartmentID = 1 },
            new Employee { ID = 2, Name = "Steve", DepartmentID = 2 },
            new Employee { ID = 3, Name = "Ben", DepartmentID = 1 },
            new Employee { ID = 4, Name = "Philip", DepartmentID = 1 },
            new Employee { ID = 5, Name = "Mary", DepartmentID = 2 },
            new Employee { ID = 6, Name = "Valarie", DepartmentID = 2 },
            new Employee { ID = 7, Name = "John", DepartmentID = 1 },
            new Employee { ID = 8, Name = "Pam", DepartmentID = 1 },
            new Employee { ID = 9, Name = "Stacey", DepartmentID = 2 },
            new Employee { ID = 10, Name = "Andy", DepartmentID = 1}
        };
        }
    }
}
