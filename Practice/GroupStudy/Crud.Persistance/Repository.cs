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
    public class Repository<TEntity, Tkey>
        : IRepository<TEntity, Tkey> where TEntity : class, IEntity<Tkey>
    {
        protected DbContext _dbContext;
        protected DbSet<TEntity> _dbSet;
        protected int CommendTimeOut { get; set; }

        public Repository(DbContext dbContext) { 
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
            CommendTimeOut = 300;           
        }
        public virtual void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public virtual void Edit(TEntity entityToUpdate)
        {
            if(_dbContext.Entry(entityToUpdate).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToUpdate);
            }
            _dbContext.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public virtual IList<TEntity> GetAll()
        {
           return _dbSet.ToList();
        }

        public virtual void Remove(Tkey id)
        {
           var entityToDelete = _dbSet.Find(id);
            Remove(entityToDelete);
        }

        public virtual void Remove(TEntity entityToDelete)
        {
            if(_dbContext.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete); 
            }
            _dbSet.Remove(entityToDelete);
        }
    }
}
