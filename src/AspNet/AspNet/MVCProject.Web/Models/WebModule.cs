using Autofac;
using MVCProject.Domain.Service;
using MVCProject.Web.Service;

namespace MVCProject.Web.Models
{
    public class WebModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //Dependency Injection by autoFac
            builder.RegisterType<Course>().As<ICourse>();
            //Dependency Injection by autoFac in Clean Architecture
            builder.RegisterType<CourseServices>().As<ICourseService>().InstancePerLifetimeScope();
        }
    }
}
