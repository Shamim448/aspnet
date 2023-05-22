using Autofac;
using Crud.web.Models;

namespace Crud.web
{
    public class WebModule : Module
    {
        private readonly string _connectionString;
        private readonly string _migrationsAssembly;
        public WebModule(string connectionString, string migrationAssembly)
        {
            _connectionString = connectionString;
            _migrationsAssembly = migrationAssembly;
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserListModel>().AsSelf().InstancePerLifetimeScope();
        }
    }
}
