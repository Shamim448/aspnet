using Autofac;
using DemoProject.Application;
using DemoProject.Application.Features.Training.Repositories;
using DemoProject.Application.Services;
using DemoProject.Domain.Services;
using DemoProject.Persistance;
using DemoProject.Persistence.Features.Training.Repositories;
using DemoProject.web.Models;

namespace DemoProject.web
{
    public class WebModule:Module
    {
        private readonly string _connectionString;
        private readonly string _migrationAssembly;
        public WebModule(string connectionString, string migrationAssembly)
        {
            _connectionString = connectionString;
            _migrationAssembly = migrationAssembly;
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<StudentListModel>().AsSelf().InstancePerLifetimeScope();  
        }
    }
}
