using Autofac;
using Autofac.Core;
using CSEData.Web.Models;

internal class WebModule :  Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);
        builder.RegisterType<DataScraper>().As<IDataScraper>().InstancePerLifetimeScope();
        //builder.RegisterType<InsertValues>().AsSelf().InstancePerLifetimeScope();

    }
}