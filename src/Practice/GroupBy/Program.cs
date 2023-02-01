// See https://aka.ms/new-console-template for more information

using GroupBy;

var result = from employee in Employee.GetAllEmployees() group employee by employee.Department;
foreach (var item in result)
{
    Console.WriteLine("{0}, {1}",item.Key, item.Count()) ;
}
