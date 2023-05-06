using DemoProject.Domain.Entities;
using DemoProject.Domain.Services;

namespace DemoProject.web.Models
{
    public class StudentListModel
    {
        private readonly IStudentService _studentService;
        public StudentListModel()
        {

        }
        public StudentListModel(IStudentService studentService)
        {
            _studentService = studentService;
        }
        public IList<Student> GetTopStudents()
        {
            return _studentService.GetStudents();
        }
    }
}
