
using SelectMany;
//Print all subject
var allSubject = Student.GetStudents().SelectMany(s => s.Subjects).Distinct().ToList();
foreach(var subject in allSubject)
{
    Console.WriteLine(subject);
}