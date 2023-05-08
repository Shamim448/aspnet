// See https://aka.ms/new-console-template for more information

using GroupBy;

var groupEmployees = from employee in Employee.GetAllEmployees() group employee by employee.Department;
foreach (var item in groupEmployees)
{
    Console.WriteLine("{0} - {1}",item.Key, item.Count()) ;
    Console.WriteLine("--------------------------");
    foreach(var employee in item)
    {
        Console.WriteLine(employee.Name + "\t" + employee.Department) ;
    }
    Console.WriteLine(); 
}

Console.WriteLine("----------department and name sorted----------------");
var employees = from employee in Employee.GetAllEmployees() group employee by employee.Department into eGroup orderby eGroup.Key ascending 
   select new{
    Key = eGroup.Key,
    Employees = eGroup.OrderBy(x => x.Name)
    };
foreach(var employee in employees)
{
    Console.WriteLine("{0} - {1}", employee.Key, employee.Employees.Count());
    Console.WriteLine("-------------------");
    foreach(var item in employee.Employees)
    {
        Console.WriteLine(item.Name + "\t" + item.Department) ;
    };
    Console.WriteLine();
}

Console.WriteLine("----------Multiple group keys----------------");
var groupEmployee = from employee in Employee.GetAllEmployees() group employee by new {employee.Department, employee.Gender} 
    into eGroup orderby eGroup.Key.Department, eGroup.Key.Gender select new {
        Dep = eGroup.Key.Department,
        Gender = eGroup.Key.Gender,
        Employee = eGroup.OrderBy(x => x.Name)
    };
foreach (var employee in groupEmployee)
{
    Console.WriteLine("{0} Department {1} Employee count = {2}", employee.Dep, employee.Gender, employee.Employee.Count() );
    Console.WriteLine("--------------------------------------------");
    foreach(var result in employee.Employee)
    {
        Console.WriteLine(result.Name + "\t" + result.Gender + "\t" + result.Department);
    }
    Console.WriteLine();
}