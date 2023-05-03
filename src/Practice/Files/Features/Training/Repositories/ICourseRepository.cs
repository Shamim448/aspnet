using FirstDemo.Domain.Entities;
using FirstDemo.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstDemo.Application.Features.Training.Repositories
{
    public interface ICourseRepository : IRepository<Course, Guid>
    {
    }
}
