﻿using Autofac;
using CSEData.Application;
using CSEData.Application.Features.Scrapping.Repositories;
using CSEData.Infrastructure;
using CSEData.Worker;
using CSEData.Worker.DataController;
using CSEData.Worker.Models;

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
        builder.RegisterType<CompanyCreateModel>().AsSelf().InstancePerLifetimeScope();
        builder.RegisterType<PriceCreateModel>().AsSelf().InstancePerLifetimeScope();
        builder.RegisterType<DataGenerateModel>().AsSelf().InstancePerLifetimeScope();
        builder.RegisterType<ValueLoadToModel>().AsSelf().InstancePerLifetimeScope();

        

        
    }
}