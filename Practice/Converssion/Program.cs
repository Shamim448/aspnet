// See https://aka.ms/new-console-template for more information
using System.Collections.Generic;
using Converssion;
List<Student> listStudent = new List<Student>
{
   new Student { StudentID= 101, Name = "Tom", TotalMarks = 800 },
   new Student { StudentID= 102, Name = "Mary", TotalMarks = 900 },
   new Student { StudentID= 103, Name = "Pam", TotalMarks = 800 }
};
var result = from studentList in listStudent select new { Key = studentList.StudentID, Value = studentList.Name };
foreach (var student in listStudent)
{
    Console.WriteLine();
}
    