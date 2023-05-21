using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud.Domain.Repositories
{
    public interface IUnitOfWork:IDisposable
    {
        void Save();
    }
}
