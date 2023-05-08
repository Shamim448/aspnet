using MVCProject.Domain.Repositories;
using MVCProject.Domain.Service;

namespace MVCProject.Web.Service
{
    public class CourseServices:ICourseService
    {
        public string Name { get; set; } = "Shamim";
        private readonly ICourseRepositories _courseRepositories;
        public CourseServices(ICourseRepositories courseRepositories)
        {
            _courseRepositories = courseRepositories;
        }
    }
}
