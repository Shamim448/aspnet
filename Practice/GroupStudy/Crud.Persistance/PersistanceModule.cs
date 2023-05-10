using Autofac;
using Crud.Application;
using Crud.Application.Features.Training.Repositories;
using Crud.Application.Services;
using Crud.Domain.Services;
using Crud.Persiatance;
using Crud.Persistance;
using Crud.Persistance.Features.Trining.Repositories;


namespace Crud.Persistance
{
    public class PersistanceModule:Module
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
            builder.RegisterType<CourseRepository>().As<ICourseRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ApplicationDbContext>().AsSelf()
               .WithParameter("connectionString", _connectionString)
               .WithParameter("migrationAssembly", _migrationsAssembly)
               .InstancePerLifetimeScope();
            builder.RegisterType<ApplicationDbContext>().As<IApplicationDbContext>()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssembly", _migrationsAssembly)
                .InstancePerLifetimeScope();
           
            builder.RegisterType<IApplicationUnitOfWork>().As<IApplicationUnitOfWork>().InstancePerLifetimeScope();
        }
    }
}
