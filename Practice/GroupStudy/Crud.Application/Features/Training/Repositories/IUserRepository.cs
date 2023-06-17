using Crud.Domain.Entities;
using Crud.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud.Application.Features.Training.Repositories
{
    public interface IUserRepository : IRepository<User , Guid>
    {
         bool IsDuplicateName(string name, Guid? id);
    }
}
