using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using Crud.Persistance;
using Crud.Application;
using Crud.Infrastructure;

namespace Crud.MigrationRunner
{
    public class Program
    {
        private static string _connectionString;
        private static string _migrationAssemblyName;
        private static IConfiguration _configuration;

        static void Main(string[] args)
        {
            _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", false)
                .AddEnvironmentVariables()
                .Build();
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
            _migrationAssemblyName = typeof(Program).Assembly.FullName;

            //Serilog configuration
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .ReadFrom.Configuration(_configuration)
                .CreateLogger();

            try {
                Log.Information("Application Starting up");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex) {
                Log.Fatal(ex, "Application start-up failed");
            }
            finally { 
                Log.CloseAndFlush();
            }
        }

        //Dependency Injection
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .UseSerilog()
            .ConfigureContainer<ContainerBuilder>(builder =>
            {
                builder.RegisterModule(new PersistanceModule(_connectionString, _migrationAssemblyName));
                builder.RegisterModule(new ApplicationModule());
                builder.RegisterModule(new InfrastructureModule());
            });
    }
}