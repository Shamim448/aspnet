using Crud.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud.Persistance
{
    public abstract class UnitOfWork : IUnitOfWork
    {
        protected readonly DbContext _dbContext;
        public UnitOfWork(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public virtual void Dispose()
        {
            _dbContext?.Dispose();
        }

        public virtual void Save()
        {
            _dbContext?.SaveChanges();
        }
    }
}
