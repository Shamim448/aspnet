using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

namespace Crud.Persistance
{
    public class PersistanceModule :Module
    {
        private readonly string _connectionString;
        private readonly string _migrationsAssembly;
        public PersistanceModule(string connectionString, string migrationsAssembly)
        {
            _connectionString = connectionString;
            _migrationsAssembly = migrationsAssembly;
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ApplicationDbContext>().AsSelf()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationsAssembly", _migrationsAssembly)
                .InstancePerLifetimeScope();
            builder.RegisterType<ApplicationDbContext>().As<IApplicationDbContext>()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationsAssembly", _migrationsAssembly)
                .InstancePerLifetimeScope();
        }
    }
}
