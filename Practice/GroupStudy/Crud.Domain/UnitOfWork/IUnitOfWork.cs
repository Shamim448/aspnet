using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud.Domain.UnitOfWork
{
    public interface IUnitOfWork:IDisposable
    {
        void Save();
    }
}
