using Crud.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud.Domain.Services
{
    public interface ICourseService
    {
        public IList<Course> GetCourses();
        
    }
}
