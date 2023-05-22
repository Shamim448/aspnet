using Autofac;
using Crud.web.Models;

namespace Crud.web
{
    public class WebModule:Module
    {
        private readonly string _connectionString;
        private readonly string _migrationsAssembly;
        public WebModule(string connectionString, string migrationsAssembly)
        {
            _connectionString = connectionString;
            _migrationsAssembly = migrationsAssembly;
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserListModel>().AsSelf().InstancePerLifetimeScope();
        }
    }
}
