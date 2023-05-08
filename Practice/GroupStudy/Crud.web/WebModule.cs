using Autofac;
using Crud.web.Models;

namespace Crud.web
{
    public class WebModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            
             builder.RegisterType<Course>().As<ICourse>();
        }
    }
}
