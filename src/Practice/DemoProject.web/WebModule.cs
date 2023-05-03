using Autofac;
using DemoProject.web.Models;

namespace DemoProject.web
{
    public class WebModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Student>().As<IStudent>();
        }
    }
}
