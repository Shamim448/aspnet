using Autofac;
using Library.web.Areas.Admin.Models;

namespace Library.web
{
    public class WebModule:Module
    {
        public WebModule() { }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<BookListModel>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<CreateBookModel>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<UpdateBookModel>().AsSelf().InstancePerLifetimeScope();
        }

    }
}
