using Autofac;
using CSEData.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSEData.Worker
{
    public class WorkerModule : Module
    {
        //private readonly string _connectionString;
        //public WorkerModule(string connectionString)
        //{
        //    _connectionString = connectionString;
        //}
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<DataService>().AsSelf().InstancePerLifetimeScope();
            
        }
    }
}
