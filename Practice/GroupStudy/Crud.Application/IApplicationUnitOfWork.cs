using Crud.Application.Features.Training.Repositories;
using Crud.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud.Application
{
    public interface IApplicationUnitOfWork:IUnitOfWork
    {
        IUserRepository Users { get; }
    }
}
