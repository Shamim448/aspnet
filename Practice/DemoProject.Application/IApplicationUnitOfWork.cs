

using DemoProject.Application.Features.Training.Repositories;
using DemoProject.Domain.UnitOfWork;

namespace DemoProject.Application
{
    public interface IApplicationUnitOfWork : IUnitOfWork
    {
        IStudentRepository Students { get; }
    }
}