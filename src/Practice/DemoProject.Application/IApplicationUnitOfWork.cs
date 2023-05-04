

using DemoProject.Application.Features.Training.Repositories;
using DemoProject.Domain.UnitOfWork;

namespace DemoProject.Application
{
    public interface IApplicationUnitOfWork : IUnitOfWork
    {
        IStudentRepository Courses { get; }
    }
}