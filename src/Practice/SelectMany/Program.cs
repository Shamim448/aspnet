
using SelectMany;
//Print all subject
var allSubject = Student.GetStudents().SelectMany(s => s.Subjects).Distinct().ToList();
//var allSubject = Student.GetStudents().SelectMany(s => s.Subjects).ToList();//all subject accept duplicates
foreach(var subject in allSubject)
{
    Console.WriteLine(subject);
}
//Print all student with subject

var studentWithSubject = Student.GetStudents().SelectMany(s => s.Subjects, (student, subject) => new { StudentName = student.Name, Subject = subject });
foreach (var v in studentWithSubject)
{
    Console.WriteLine(v.StudentName + " - " + v.Subject);
}