using Crud.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud.Domain.Repositories
{
    public interface IRepository<TEntity, Tkey> 
        where TEntity : class, IEntity<Tkey>
    {
        void Add(TEntity entity);
        void Remove(Tkey id);
        void Remove(TEntity entityToDelete);
        void Edit(TEntity entityToUpdate);
        IList<TEntity>GetAll();
    }
}
