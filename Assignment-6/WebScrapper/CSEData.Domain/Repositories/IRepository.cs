using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSEData.Domain.Repositories
{
    public interface IRepository <TEntity, TKey> 
        where TEntity : class, IEntity<TKey>
    {
        void Add(TEntity entity);
        Task AddAsync(TEntity entity);
        void AddRange(IList<TEntity> entity);
        void Remove(TEntity entityToDelete);
        void Remove(TKey id);
        IList<TEntity> GetAll();
        Task<IList<TEntity>> GetAllAsync();
        TEntity GetById(TKey id);
    }
}
