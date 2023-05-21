using Crud.Domain.Entities;
using Crud.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud.Persistance
{
    public abstract class Repository<TEntity, TKey> 
        : IRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        protected  DbContext _dbContext;
        protected  DbSet<TEntity> _dbSet;
        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>(); 
        }
        public void Add(TEntity entity)
        {
            _dbSet.Add(entity); 
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if(_dbContext.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }
            _dbSet.Remove(entityToDelete);
        }

        public virtual void Delete(TKey id)
        {
            var entityToDelete = _dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual IList<TEntity> GetAll()
        {
            return _dbSet.ToList();
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            if(_dbContext.Entry(entityToUpdate).State == EntityState.Detached) 
            { 
                _dbSet.Attach(entityToUpdate);
            }
            _dbContext.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}
