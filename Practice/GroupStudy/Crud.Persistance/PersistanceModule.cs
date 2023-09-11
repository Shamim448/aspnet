using Autofac;
using Crud.Application;
using Crud.Application.Features.Training.Repositories;
using Crud.Domain.Utilities;
using Crud.Persistance.Features.Training.Repositories;
using Microsoft.EntityFrameworkCore;
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
        public PersistanceModule(string connectionString, string migrationAssembly)
        {
            _connectionString = connectionString;
            _migrationsAssembly = migrationAssembly;
        }
        protected override void Load(ContainerBuilder builder)
        {
           builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerLifetimeScope();
           
            builder.RegisterType<ApplicationDbContext>().AsSelf()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssembly", _migrationsAssembly)
                .InstancePerLifetimeScope();
            builder.RegisterType<ApplicationDbContext>().As<IApplicationDbContext>()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssembly", _migrationsAssembly)
                .InstancePerLifetimeScope();
            //use this for Adonetutility (Collect db context from other object)
            builder.Register(x => new AdoNetUtility(x.Resolve<ApplicationDbContext>().Database.GetDbConnection(), 300))
                .As<IAdoNetUtility>().InstancePerLifetimeScope();

            builder.RegisterType<ApplicationUnitOfWork>().As<IApplicationUnitOfWork>().InstancePerLifetimeScope();
        }
    }
}
