﻿using Autofac;
using Crud.Application;
using Crud.Application.Features.Training.Repositories;
using Crud.Application.Services;
using Crud.Domain.Services;
using Crud.Persiatance;
using Crud.Persistance;
using Crud.Persistance.Features.Trining.Repositories;
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
            builder.RegisterType<CourseModelList>().AsSelf().InstancePerLifetimeScope();  
        }
    }
}
