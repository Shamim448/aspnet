using Autofac;
using Library.Application.Features.Inventory.Services;
using Library.Infrastructure.Features.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure
{
    public class InfrastructureModule:Module
    {
        public InfrastructureModule() 
        {
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<BookService>().As<IBookService>().InstancePerLifetimeScope();
        }
    }
}
