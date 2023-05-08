// See https://aka.ms/new-console-template for more information
using GroupJoin;

var employeeByDepartment = from d in Department.GetAllDepartments() join e in Employee.GetAllEmployees() on d.ID equals e.DepartmentID 
        into eGroup select new
            {
                Department = d,
                Employee = eGroup
        };
foreach(var department in employeeByDepartment)
{
    Console.WriteLine(department.Department.Name);
    foreach(var employee in department.Employee)
    {
        Console.WriteLine(" " + employee.Name);
    }
}
Console.WriteLine("----------------Inner Join-----------------------");
var names = from a in Department.GetAllDepartments() join b in Employee.GetAllEmployees() on a.ID equals b.DepartmentID 
            select new { DepartmentName = a.Name, EmployeeName = b.Name };
foreach(var name in names) 
{
    Console.WriteLine(name.EmployeeName + "\t" + name.DepartmentName);
    //payroll department name will missing
}