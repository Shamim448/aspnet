using Autofac;
using Autofac.Extensions.DependencyInjection;
using Crud.EmailWorker;
using Microsoft.Extensions.Logging.Configuration;
using Serilog;
using Serilog.Events;

////connect appsetting file
//var configuration =  new ConfigurationBuilder().AddJsonFile("appsettings.json", false)
//                .AddEnvironmentVariables()
//                .Build();
//var connectionString = configuration.GetConnectionString("DefaultConnection");
//var migrationAssemblyName = typeof(Worker).Assembly.FullName;
////serilog
//Log.Logger = new LoggerConfiguration()
//    .MinimumLevel.Debug()
//    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
//    .Enrich.FromLogContext()
//    .ReadFrom.Configuration(configuration)//serilog.setting.configuration package
//    .CreateLogger();

//try {
//    Log.Information("Application Starting Up");

//    IHost host = Host.CreateDefaultBuilder(args)
//        .UseWindowsService() //extention.hosting,. windowsService package
//        .UseServiceProviderFactory(new AutofacServiceProviderFactory())
//        .UseSerilog() //serilog extention hosting package
//        .ConfigureContainer<ContainerBuilder>(builder =>
//        {
//            builder.RegisterModule(new WorkerModule());
//        })
//        .ConfigureServices(services =>
//        {
//            services.AddHostedService<Worker>();
//        })
//        .Build();
//    await host.RunAsync();
//}
//catch (Exception ex)
//{
//    Log.Fatal(ex, "Application start-up failed");
//}
//finally
//{
//    Log.CloseAndFlush();
//}


var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", false)
                .AddEnvironmentVariables()
                .Build();

var connectionString = configuration.GetConnectionString("DefaultConnection");

var migrationAssemblyName = typeof(Worker).Assembly.FullName;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();

try
{
    Log.Information("Application Starting up");

    IHost host = Host.CreateDefaultBuilder(args)
        .UseWindowsService()
        .UseServiceProviderFactory(new AutofacServiceProviderFactory())
        .UseSerilog()
        .ConfigureContainer<ContainerBuilder>(builder =>
        {
            builder.RegisterModule(new WorkerModule());
        })
        .ConfigureServices(services =>
        {
            services.AddHostedService<Worker>();
        })
        .Build();

    await host.RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application start-up failed");
}
finally
{
    Log.CloseAndFlush();
}