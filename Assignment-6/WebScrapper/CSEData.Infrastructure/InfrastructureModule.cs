using Autofac;
using CSEData.Application.Features.Scrapping.Repositories;
using CSEData.Application;
using CSEData.Application.Services;
using CSEData.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSEData.Infrastructure.Features;
using CSEData.Infrastructure.Features.Scrapping.Repositories;
using System.Reflection;

namespace CSEData.Infrastructure
{
    public class InfrastructureModule: Autofac.Module
    {
        private readonly string _connectionString;
        private readonly string _assemblyName;
        public InfrastructureModule(string connectionString, string assemblyName) 
        {
            _connectionString = connectionString;
            _assemblyName = assemblyName;
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CompanyService>().As<ICompanyService>()
                .InstancePerLifetimeScope();
           
            builder.RegisterType<PriceService>().As<IPriceService>()
                .InstancePerLifetimeScope();

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
