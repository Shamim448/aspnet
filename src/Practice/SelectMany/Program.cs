
using SelectMany;
//Print all subject
var result = from student in Student.GetStudents() from subject  in student.Subjects 
             select subject.Distinct() ;
foreach (var item in result)
{
    Console.WriteLine(item);
}
var s = from student in Student.GetStudents() select student.Name;
foreach (var item in s)
{
    Console.WriteLine(item);
}

string[] a = { "shamim", "Saba", "Sopna"};
var c = from d in a select d;
foreach (var item in c) { Console.WriteLine(item); }


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