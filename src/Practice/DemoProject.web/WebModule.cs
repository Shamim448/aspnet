using Autofac;
using DemoProject.Application.Features.Training.Repositories;
using DemoProject.Application.Services;
using DemoProject.Domain.Services;
using DemoProject.Persistence.Features.Training.Repositories;

namespace DemoProject.web
{
    public class WebModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<StudentRepository>().As<IStudentRepository>().InstancePerLifetimeScope();
            builder.RegisterType<StudentService>().As<IStudentService>().InstancePerLifetimeScope();
        }
    }
}
