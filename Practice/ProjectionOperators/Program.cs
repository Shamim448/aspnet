
using ProjectionOperators;
//Print all employee id
IEnumerable<int> allEmployeeID = Employee.GetAllEmployees().Select(emm => emm.EmployeeID);
foreach (int employeeID in allEmployeeID)
{
    Console.WriteLine(employeeID);
}

//Print First name and gender
var FirstNameAndGender = Employee.GetAllEmployees().Select(emm => new { FirstNamr = emm.FirstName, Gender = emm.Gender });
foreach (var value in FirstNameAndGender) 
{ 
    Console.WriteLine(value.FirstNamr + " - " + value.Gender); 
}
//Print Full name annual salary and give 10% bonus whose salary getter then 50000
var resuelt = Employee.GetAllEmployees().Where(emm => emm.AnnualSalary > 50000).
    Select(emm => new {FullName = emm.FirstName+" "+emm.LastName, AnnualSalary = emm.AnnualSalary, Bonue = emm.AnnualSalary*.1});
foreach(var value in resuelt)
{
    Console.WriteLine(value.FullName + " - " + value.AnnualSalary + " - " + value.Bonue);
}