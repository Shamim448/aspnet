
using Autofac;
using DemoProject.Application.Features.Training.Repositories;
using DemoProject.Application.Services;
using DemoProject.Domain.Services;
namespace DemoProject.Application
{
    public class ApplicationModule : Module
    {
		private readonly string _connectionString;
		private readonly string _migrationAssemblyName;
		public ApplicationModule(string connectionString, string migrationAssemblyName)
		{
			_connectionString = connectionString;
			_migrationAssemblyName = migrationAssemblyName;
		}
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<StudentService>().As<IStudentService>()
                .InstancePerLifetimeScope();
		}
    }
}
