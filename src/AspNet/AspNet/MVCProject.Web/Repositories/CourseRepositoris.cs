using MVCProject.Domain.Repositories;
using MVCProject.Web.Models;

namespace MVCProject.Web.Repositories
{
    public class CourseRepositoris: ICourseRepositories
    {
        private readonly ICourse _course;
        public CourseRepositoris(ICourse Course) { 
            _course= Course;
        }
    }
}
