using Autofac;

namespace Crud.API.Models
{
    public class ApiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserModel>().AsSelf();
            builder.RegisterType<EnrollmentModel>().AsSelf();
            base.Load(builder);
        }
    }
}
