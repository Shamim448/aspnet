 using Crud.Domain.Entities;
using Crud.Domain.Services;

namespace Crud.web.Models
{
    public class CourseModelList
    {
        private readonly ICourseService _courseService;
        public CourseModelList() { 
        
        }
        public CourseModelList(ICourseService courseService)
        {
            _courseService = courseService;
        }
        public IList<Course> GetPopularCourses()
        {
            return _courseService.GetCourses();
        }
    }
}
