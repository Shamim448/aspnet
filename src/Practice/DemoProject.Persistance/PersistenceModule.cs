using Autofac;
using DemoProject.Application;
using DemoProject.Application.Features.Training.Repositories;
using DemoProject.Persistence.Features.Training.Repositories;

namespace DemoProject.Persistance
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
			builder.RegisterType<StudentRepository>().As<IStudentRepository>()
				.InstancePerLifetimeScope();

			builder.RegisterType<ApplicationDbContext>().AsSelf()
				.WithParameter("connectionString", _connectionString)
				.WithParameter("migrationAssembly", _migrationAssemblyName)
				.InstancePerLifetimeScope();

			builder.RegisterType<ApplicationDbContext>().As<IApplicationDbContext>()
				.WithParameter("connectionString", _connectionString)
				.WithParameter("migrationAssembly  ", _migrationAssemblyName)
				.InstancePerLifetimeScope();

			builder.RegisterType<ApplicationUnitOfWork>().As<IApplicationUnitOfWork>()
				.InstancePerLifetimeScope();
		}
    }
}
