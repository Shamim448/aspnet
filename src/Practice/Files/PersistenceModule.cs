using Autofac;
using FirstDemo.Application;
using FirstDemo.Application.Features.Training.Repositories;
using FirstDemo.Application.Services;
using FirstDemo.Domain.Services;
using FirstDemo.Persistence.Features.Training.Repositories;
using FirstDemo.Persistence;

namespace FirstDemo.Persistence
{
    public class PersistenceModule : Module
    {
		private readonly string _connectionString;
		private readonly string _migrationAssemblyName;

		public PersistenceModule(string connectionString, string migrationAssemblyName)
		{
			_connectionString = connectionString;
			_migrationAssemblyName = migrationAssemblyName;
		}

        protected override void Load(ContainerBuilder builder)
        {
			builder.RegisterType<CourseRepository>().As<ICourseRepository>()
				.InstancePerLifetimeScope();

			builder.RegisterType<ApplicationDbContext>().AsSelf()
				.WithParameter("connectionString", _connectionString)
				.WithParameter("migrationAssembly", _migrationAssemblyName)
				.InstancePerLifetimeScope();

			builder.RegisterType<ApplicationDbContext>().As<IApplicationDbContext>()
				.WithParameter("connectionString", _connectionString)
				.WithParameter("migrationAssembly", _migrationAssemblyName)
				.InstancePerLifetimeScope();

			builder.RegisterType<ApplicationUnitOfWork>().As<IApplicationUnitOfWork>()
				.InstancePerLifetimeScope();
		}
    }
}
