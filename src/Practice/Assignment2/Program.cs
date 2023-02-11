// See https://aka.ms/new-console-template for more information
using Assignment2;
using System.Reflection;

Course course = new Course
{
    Title = "Asp.Net",
    Fees = 30000.00,
    Teacher = new Instructor
    {
        Name = "Jalal Uddin",
        Email = "info@devskill.com"
    }
    

};
string xml = XMLFormatter.Convert(course);
Console.WriteLine(xml);

