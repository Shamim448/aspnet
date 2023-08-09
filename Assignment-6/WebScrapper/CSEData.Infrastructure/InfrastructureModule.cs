using Autofac;
using CSEData.Application.Services;
using CSEData.Infrastructure.Services;

namespace CSEData.Infrastructure
{
    public class InfrastructureModule: Module
    {

        public InfrastructureModule() {     }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CompanyService>().As<ICompanyService>()
                .InstancePerLifetimeScope();
           
            builder.RegisterType<PriceService>().As<IPriceService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<NodeGenaratorService>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<WebScraperService>().AsSelf().InstancePerLifetimeScope();
        }
    }
}
