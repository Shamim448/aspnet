using Crud.Application.Features.Training.Repositories;
using Crud.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud.Persistance.Features.Training.Repositories
{
    public class UserRepository : Repository<User, int>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }
            public bool IsDuplicateName(string name, int? id)
            {
                int? exeistingValue = null;
                if (id.HasValue)
                    exeistingValue = GetCount(x => x.Name == name && x.Id != id.Value);
                else           
                    exeistingValue = GetCount(x => x.Name == name);
                    return exeistingValue > 0;
                
            }
        
    }
}
