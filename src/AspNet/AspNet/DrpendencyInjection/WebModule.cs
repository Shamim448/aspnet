using Autofac;
using DrpendencyInjection.Application.Services;
using DrpendencyInjection.domain.Repositories;
using DrpendencyInjection.domain.Services;
using DrpendencyInjection.Models;
using DrpendencyInjection.Repositories;

namespace DrpendencyInjection
{
    public class WebModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CourseRepositories>().As<ICourseRepositories>().InstancePerLifetimeScope();
            builder.RegisterType<CourseServices>().As<ICourseServices>().InstancePerLifetimeScope();
        }
    }
}
