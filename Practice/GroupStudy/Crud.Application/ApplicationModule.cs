﻿using Autofac;
using Crud.Application.Features.Training.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud.Application
{
    public class ApplicationModule : Module
    {       
        public ApplicationModule()
        {
            
        }
        protected override void Load(ContainerBuilder builder)
        {
          //  builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();

        }

        
    }
}
