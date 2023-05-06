using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectionOperators
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public int AnnualSalary { get; set; }

        public static List<Employee> GetAllEmployees()
        {
            List<Employee> listEmployee = new List<Employee>
            {
                new Employee
                {
                    EmployeeID = 101,
                    FirstName = "Tom",
                    LastName = "Daely",
                    Gender = "Male",
                    AnnualSalary = 60000
                },
                new Employee
                {
                    EmployeeID = 102,
                    FirstName = "Mike",
                    LastName = "Mist",
                    Gender = "Male",
                    AnnualSalary = 72000
                },
                new Employee
                {
                    EmployeeID = 103,
                    FirstName = "Mary",
                    LastName = "Lambeth",
                    Gender = "Female",
                    AnnualSalary = 48000
                },
                new Employee
                {
                    EmployeeID = 104,
                    FirstName = "Pam",
                    LastName = "Penny",
                    Gender = "Female",
                    AnnualSalary = 84000
                },

            };
            return listEmployee;
        }
    }
}
