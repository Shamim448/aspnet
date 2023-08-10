using Autofac;
using Autofac.Extensions.DependencyInjection;
using CSEData.Infrastructure;
using CSEData.Persistance;
using CSEData.Worker;
using Microsoft.EntityFrameworkCore;
using Serilog.Events;
using Serilog;
using CSEData.Application.Services;
using CSEData.Infrastructure.Services;
using System.Reflection;



//Load Appsetting
var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", false)
                .AddEnvironmentVariables()
                .Build();

//Get ConnectionString
var connectionString = configuration.GetConnectionString("DefaultConnection");
//var assemblyName = typeof(Worker).Assembly.FullName;
var assemblyName = Assembly.GetExecutingAssembly().FullName;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();

try {
    Log.Information("Application Starting up");
    IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService()
    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .UseSerilog()
    .ConfigureContainer<ContainerBuilder>(builder =>
    {
        builder.RegisterModule(new WorkerModule(configuration));
        builder.RegisterModule(new InfrastructureModule());
        builder.RegisterModule(new PersistanceModule(connectionString, assemblyName));


    })
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        services.AddSingleton<IWebScraperService, WebScraperService>();

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString, m => m.MigrationsAssembly(assemblyName)));
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

