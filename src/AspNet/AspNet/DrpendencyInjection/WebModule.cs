using Autofac;
using DrpendencyInjection.Models;

namespace DrpendencyInjection
{
    public class WebModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Course>().As<ICourse>();
        }
    }
}
