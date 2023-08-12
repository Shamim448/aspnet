
using CSEData.Worker;
using Serilog;
using Serilog.Events;

//connect Appsetting
var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", false)
    .AddEnvironmentVariables()
    .Build();
var connectionString = configuration.GetConnectionString("DefaultConnection");
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.File(configuration["Logging:Logpath"])
    .CreateLogger();
IHost host = Host.CreateDefaultBuilder(args)
    //.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    //.ConfigureContainer<ContainerBuilder>(builder =>
    //{
    //    builder.RegisterModule(new WorkerModule());
    //    //builder.RegisterModule(new WebModule(connectionString));
    //})
    .ConfigureServices(services =>
    {
        services.AddHostedService<DataService>();        
        //services.AddHostedService<Worker>();
    })
    .UseSerilog()
    .Build();


host.Run();
