using DemoProject.Domain.Repositories;
using DemoProject.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoProject.Application.Services
{
    public class StudentService:IStudentService
    {
        private readonly IStudentRepository _studentRepository; 
        public StudentService(IStudentRepository studentRepository) {
            _studentRepository = studentRepository;
        }
    }
}
