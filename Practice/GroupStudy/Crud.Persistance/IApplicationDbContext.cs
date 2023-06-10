using Crud.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud.Persistance
{
    public interface IApplicationDbContext
    {
       public DbSet<Student> Students { get; }
    }
}
