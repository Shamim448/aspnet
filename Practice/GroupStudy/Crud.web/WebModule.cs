using Autofac;
using Crud.web.Areas.Admin.Models;
using Crud.web.Models;

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
            builder.RegisterType<UserCreateModel>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<UserUpdateModel>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<RegisterModel>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<LoginModel>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<CreateRoleModel>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<RoleListModel>().AsSelf().InstancePerLifetimeScope();
        }
    }
}