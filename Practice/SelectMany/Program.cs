
using SelectMany;

IEnumerable<Student> results = from st in Student.GetStudents() orderby st.Mark, st.Name, st.Id descending select st;
foreach( var st in results)
{
    Console.WriteLine(st.Mark + " " + st.Name + " " + st.Id);
}
Console.WriteLine("-------------------");
//Print all unique subject
var result = (from student in Student.GetStudents()
                             from subject in student.Subjects
             select subject).Distinct();
foreach (var item in result)
{
    Console.WriteLine(item);
}
Console.WriteLine("-------------------");
Console.WriteLine("Print All Student Name ");
var s = from student in Student.GetStudents() orderby student.Name select student.Name;
foreach (var item in s)
{
    Console.WriteLine(item);
}
Console.WriteLine("--------first 2 name-----------");
//print first 2 name
string[] a = { "shamim", "Saba", "Sopna"};
IEnumerable<string> c = (from d in a select d).Take(2);
foreach (var item in c) { Console.WriteLine(item); }
Console.WriteLine("-------------------");
var allSubject = Student.GetStudents().SelectMany(s => s.Subjects).Distinct().ToList();
//var allSubject = Student.GetStudents().SelectMany(s => s.Subjects).ToList();//all subject accept duplicates
foreach(var subject in allSubject)
{
    Console.WriteLine(subject);
}
//Print all student with subject
Console.WriteLine("-------Print all student with subject------------");
var studentWithSubject = Student.GetStudents().SelectMany(s => s.Subjects, (student, subject) => new { StudentName = student.Name, Subject = subject });
foreach (var v in studentWithSubject)
{
    Console.WriteLine(v.StudentName + " - " + v.Subject);
}