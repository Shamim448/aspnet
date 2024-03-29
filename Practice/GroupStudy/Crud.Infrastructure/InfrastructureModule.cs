﻿using Autofac;
using Crud.Application.Features.Training.Services;
using Crud.Infrastructure.Features.Services;
using Crud.Infrastructure.Securities;

namespace Crud.Infrastructure
{
    public class InfrastructureModule : Module
    {     
        public InfrastructureModule()
        {
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
            builder.RegisterType<TokenService>().As<ITokenService>().InstancePerLifetimeScope();

        }

        
    }
}
