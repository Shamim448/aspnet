// See https://aka.ms/new-console-template for more information
Course course = new Course();
course.Title = "Asp.Net";
var orm = new MyORM();
var result = orm.Insert(course);
Console.WriteLine(result);