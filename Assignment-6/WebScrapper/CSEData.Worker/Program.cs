using Autofac;
using Autofac.Extensions.DependencyInjection;
using CSEData.Infrastructure;
using CSEData.Persistance;
using CSEData.Worker;
using Microsoft.EntityFrameworkCore;
using System.Reflection;


//Load Appsetting
var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", false)
                .AddEnvironmentVariables()
                .Build();

//Get ConnectionString
var connectionString = configuration.GetConnectionString("DefaultConnection");
//var assemblyName = typeof(Worker).Assembly.FullName;
var assemblyName = Assembly.GetExecutingAssembly().FullName;


IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService()
    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(builder =>
    {
        builder.RegisterModule(new WorkerModule(configuration));
        builder.RegisterModule(new InfrastructureModule());
        builder.RegisterModule(new PersistanceModule(connectionString, assemblyName));


    })
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString, m => m.MigrationsAssembly(assemblyName)));
    })
    .Build();

await host.RunAsync();
