using Autofac;
using DemoProject.web.Models;

namespace DemoProject.web
{
    public class WebModule : Module
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;
        public WebModule(string connectionString, string migrationAssembly)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssembly;
        }
        protected override void Load(ContainerBuilder builder)
        {
           builder.RegisterType<StudentListModel>().AsSelf().InstancePerLifetimeScope();  
        }
    }
}
