using Autofac;
using FirstDemo.Application;
using FirstDemo.Application.Features.Training.Repositories;
using FirstDemo.Application.Services;
using FirstDemo.Domain.Services;

namespace FirstDemo.Application
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
            builder.RegisterType<CourseService>().As<ICourseService>()
                .InstancePerLifetimeScope();
		}
    }
}
