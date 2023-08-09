﻿using Autofac;
using CSEData.Worker;


internal class WorkerModule : Module
{

    private readonly IConfiguration _configuration;
    public WorkerModule( IConfiguration configuration)
    {     
        _configuration = configuration;
    }
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);
        builder.RegisterType<Worker>().AsSelf().InstancePerLifetimeScope();
        //builder.RegisterType<CompanyCreateModel>().AsSelf().InstancePerLifetimeScope();
        //builder.RegisterType<PriceCreateModel>().AsSelf().InstancePerLifetimeScope();     
        //builder.RegisterType<WebScraperModel>().AsSelf().InstancePerLifetimeScope();     
    }
}