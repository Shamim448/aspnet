using Autofac;
using CSEData.Application;
using CSEData.Application.Features.Scrapping.Repositories;
using CSEData.Persistance.Features.Scrapping.Repositories;

namespace CSEData.Persistance
{
    public class PersistanceModule: Autofac.Module
    {
        private readonly string _connectionString;
        private readonly string _assemblyName;
        public PersistanceModule(string connectionString, string assemblyName) 
        {
            _connectionString = connectionString;
            _assemblyName = assemblyName;
        }
        protected override void Load(ContainerBuilder builder)
        {


            builder.RegisterType<ApplicationUnitOfWork>().As<IApplicationUnitOfWork>()
            .InstancePerLifetimeScope();

            builder.RegisterType<CompanyRepository>().As<ICompanyRepository>()
                .InstancePerLifetimeScope();
            builder.RegisterType<PriceRepository>().As<IPriceRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ApplicationDbContext>().AsSelf()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("assemblyName", _assemblyName)
                .InstancePerLifetimeScope();

            builder.RegisterType<ApplicationDbContext>().As<IApplicationDbContext>()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("assemblyName", _assemblyName)
                .InstancePerLifetimeScope();
        }
    }
}
