using assignment7.Models.SQS;
using Autofac;
using Autofac.Core;

public class WebModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {

        builder.RegisterType<CreateQueueModel>().AsSelf().InstancePerLifetimeScope();
        builder.RegisterType<UserInfo>().AsSelf().InstancePerLifetimeScope();
        builder.RegisterType<ReceiveMessage>().AsSelf().InstancePerLifetimeScope();
    }
}