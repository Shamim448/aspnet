using Autofac;
using MVCProject.Domain.Repositories;
using MVCProject.Domain.Service;
using MVCProject.Web.Repositories;
using MVCProject.Web.Service;

namespace MVCProject.Web.Models
{
    public class WebModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //Dependency Injection by autoFac
            
            //Dependency Injection by autoFac in Clean Architecture
            builder.RegisterType<CourseRepositoris>().As<ICourseRepositories>().InstancePerLifetimeScope();
            builder.RegisterType<CourseServices>().As<ICourseService>().InstancePerLifetimeScope();
            builder.RegisterType<Course>().As<ICourse>();
        }
    }
}
