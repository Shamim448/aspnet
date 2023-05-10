using Autofac;
using Crud.Application.Features.Training.Repositories;
using Crud.Application.Services;
using Crud.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud.Application
{
    public class ApplicationModule:Module
    {
        private readonly string _connectionString;
        private readonly string _migrationsAssembly;
        public ApplicationModule(string connectionString, string migrationsAssembly)
        {
            _connectionString = connectionString;
            _migrationsAssembly = migrationsAssembly;
        }
        protected override void Load(ContainerBuilder builder)
        {
           
            builder.RegisterType<CourseService>().As<ICourseService>().InstancePerLifetimeScope();
            
        }
    }
}
