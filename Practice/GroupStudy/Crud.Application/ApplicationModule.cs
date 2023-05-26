using Autofac;

namespace Crud.Application
{
    public class ApplicationModule : Module
    {       
        public ApplicationModule()
        {
            
        }
        protected override void Load(ContainerBuilder builder)
        {
          //  builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();

        }

        
    }
}
