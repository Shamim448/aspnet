using DemoProject.Application.Features.Training.Repositories;
using DemoProject.Domain.Entities;
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
        private readonly IApplicationUnitOfWork _unitOfWork; 
        public StudentService(IApplicationUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        } 

        public IList<Student> GetStudents()
        {
           return _unitOfWork.Students.GetAll();
        }
        //Used for 
        public async Task<(IList<Student> records, int total, int totalDisplay)>
            GetPagedStudentAsync(int pageIndex, int pageSize, string searchText, string orderBy)
        {
            return await Task.Run(() => {
                var result = _unitOfWork.Students.GetDynamic(x => x.Name.Contains(searchText),
                orderBy, string.Empty, pageIndex, pageSize, true);

                return result;
            });
        }
    }
}
