using Autofac;
using CSEData.Web.Data;

namespace CSEData.Web
{
    public class WebModule:Module
    {
        private readonly string _connectionString;
        public WebModule(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ApplicationDbContext>().AsSelf()
                .WithParameter("connectionString", _connectionString)
                .InstancePerLifetimeScope();
            builder.RegisterType<ApplicationDbContext>().As<IApplicationDbContext>()
                .WithParameter("connectionString", _connectionString)
                .InstancePerLifetimeScope();
        }
    }
}
