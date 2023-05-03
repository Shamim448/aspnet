using Autofac;
using DemoProject.Application.Services;
using DemoProject.Domain.Entities;
using DemoProject.Domain.Repositories;
using DemoProject.Domain.Services;
using DemoProject.web.Models;
using DemoProject.web.Repositories;

namespace DemoProject.web
{
    public class WebModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Student>().As<IStudent>().InstancePerLifetimeScope();
            builder.RegisterType<StudentRepository>().As<IStudentRepository>().InstancePerLifetimeScope();
            builder.RegisterType<StudentService>().As<IStudentService>().InstancePerLifetimeScope();
        }
    }
}
