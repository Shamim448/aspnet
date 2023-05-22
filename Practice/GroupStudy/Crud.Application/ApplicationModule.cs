using Autofac;
using Crud.Application.Services;
using Crud.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud.Application
{
    public class ApplicationModule : Module
    {
        private readonly string _connectionString;
        private readonly string _migrationAssembly;
        public ApplicationModule(string connectionString, string migrationsAssembly)
        {
            _connectionString = connectionString;
            _migrationAssembly = migrationsAssembly;
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();

        }

        
    }
}
