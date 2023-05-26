using Autofac;
using Crud.web.Areas.Admin.Models;

namespace Crud.web
{
    public class WebModule : Module
    {      
        public WebModule()
        {

        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserListModel>().AsSelf().InstancePerLifetimeScope();
        }
    }
}