using Autofac;

namespace MVCProject.Web.Models
{
    public class WebModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Course>().As<ICourse>();
        }
    }
}
