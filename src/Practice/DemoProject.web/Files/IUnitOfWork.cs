using System;
using System.Collections.Generic;
using System.Text;

namespace FirstDemo.Domain.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();
    }
}
