using Crud.Application;
using Crud.Application.Features.Training.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud.Persistance
{
    public class ApplicationUnitOfWork : UnitOfWork, IApplicationUnitOfWork
    {
        public IUserRepository Users { get; private set; }
        public ApplicationUnitOfWork(IApplicationDbContext dbContext, IUserRepository userRepository)
            : base((DbContext)dbContext)
        {
            Users = userRepository;
        }      
    }
    
}
