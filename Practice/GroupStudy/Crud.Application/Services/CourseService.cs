using Crud.Domain.Entities;
using Crud.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud.Application.Services
{
    public class CourseService : ICourseService
    {
        private readonly IApplicationUnitOfWork _unitOfWork;
        public CourseService(IApplicationUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IList<Course> GetCourses()
        {
            return _unitOfWork.Courses.GetAll();
        }

        
        //Used for 

    }
}
