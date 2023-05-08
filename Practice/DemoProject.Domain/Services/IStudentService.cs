using DemoProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoProject.Domain.Services
{
    public interface IStudentService
    {
        public IList<Student> GetStudents();
        Task <(IList<Student> records, int total, int totalDisplay)> 
            GetPagedStudentAsync(int pageIndex, int pageSize, string searchText, string orderBy);
    }
}
