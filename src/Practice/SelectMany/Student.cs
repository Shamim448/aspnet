using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelectMany
{
    public class Student
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Id { get; set; }
        public double Mark { get; set; }
        public List<string> Subjects { get; set; }
        public static List<Student> GetStudents()
        {
            List<Student> listStudents = new List<Student> { 
                new Student
                {
                    Name = "Shamim",
                    Gender = "Male",
                    Id = 101,
                    Mark = 800.00,
                    Subjects= new List<string> {"Asp.Net", "C#"}
                },
              
               new Student
                {
                    Name = "Tom",
                    Gender = "Male",
                    Id = 102,
                    Mark = 800.00,
                    Subjects = new List<string> { "ASP.NET", "C#" }
                },
                new Student
                {
                    Name = "Mike",
                    Gender = "Male",
                    Id = 103,
                    Mark = 900.00,
                    Subjects = new List<string> { "ADO.NET", "C#", "AJAX" }
                },
                new Student
                {
                    Name = "Pam",
                    Gender = "Female",
                    Id = 104,
                    Mark = 900.00,
                    Subjects = new List<string> { "WCF", "SQL Server", "C#" }
                },
                new Student
                {
                    Name = "Mary",
                    Gender = "Female",
                    Id = 105,
                    Mark = 800.00,
                    Subjects = new List<string> { "WPF", "LINQ", "ASP.NET" }
                },
                   new Student
                {
                    Name = "Shamim",
                    Gender = "Male",
                    Id = 106,
                    Mark = 800.00,
                    Subjects= new List<string> {"Asp.Net", "C#"}
                },
            };
            return listStudents;
        }
    }
}
