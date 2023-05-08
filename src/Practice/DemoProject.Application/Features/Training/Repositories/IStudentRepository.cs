using DemoProject.Application.Services;
using DemoProject.Domain.Entities;
using DemoProject.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoProject.Application.Features.Training.Repositories
{
    public interface IStudentRepository: IRepository<Student, Guid>
    {
    }

}
