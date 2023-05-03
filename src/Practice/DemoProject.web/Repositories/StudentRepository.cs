using DemoProject.Domain.Entities;
using DemoProject.Domain.Repositories;
using DemoProject.web.Models;

namespace DemoProject.web.Repositories
{
    public class StudentRepository:IStudentRepository
    {
        private readonly IStudent _student;
        public StudentRepository(IStudent student) {
            _student = student;
        }
    }
}
