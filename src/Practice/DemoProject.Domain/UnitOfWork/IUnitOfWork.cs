using System;
using System.Collections.Generic;
using System.Text;

namespace DemoProject.Domain.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();
    }
}
