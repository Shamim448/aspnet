using Autofac;
using Crud.Application.Features.Training.Services;
using Crud.Infrastructure.Features.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        }

        
    }
}
