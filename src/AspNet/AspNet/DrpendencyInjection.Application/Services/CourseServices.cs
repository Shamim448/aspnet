using DrpendencyInjection.domain.Repositories;
using DrpendencyInjection.domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrpendencyInjection.Application.Services
{
    public class CourseServices:ICourseServices
    {
        private readonly ICourseRepositories _courseRepositories;
        public CourseServices(ICourseRepositories courseRepositories) {
            _courseRepositories = courseRepositories;
        }
    }
}
