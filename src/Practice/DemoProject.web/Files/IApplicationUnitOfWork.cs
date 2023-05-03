
using FirstDemo.Application.Features.Training.Repositories;
using FirstDemo.Domain.UnitOfWorks;

namespace FirstDemo.Application
{
    public interface IApplicationUnitOfWork : IUnitOfWork
    {
        ICourseRepository Courses { get; }
    }
}