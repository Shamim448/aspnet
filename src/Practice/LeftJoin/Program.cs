// See https://aka.ms/new-console-template for more information
//Left outer join
using LeftJoin;

var names = from empployee in Employee.GetAllEmployees() 
            join department in Department.GetAllDepartments() on
            empployee.DepartmentID equals department.ID  into eGroup
            from department in eGroup.DefaultIfEmpty()
            
            select new { EmployeeName = empployee.Name, DepartmentName = department == null? "No Department" : department.Name };
foreach(var name in names)
{
    Console.WriteLine(name.EmployeeName + "\t" + name.DepartmentName);
}
Console.WriteLine("---------------Cross Join----------");
var crossJoin = from e in Employee.GetAllEmployees()
            from d in Department.GetAllDepartments() select new { e, d};
foreach(var e in crossJoin)
{
    Console.WriteLine(e.e.Name + "\t" + e.d.Name);
}