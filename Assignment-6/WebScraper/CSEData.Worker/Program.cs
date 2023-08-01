using CSEData.Worker;
using Serilog;
using Serilog.Events;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<DataService>();
        //services.AddHostedService<Worker>();
    })
    .UseSerilog()
    .Build();
//connect Appsetting
var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", false)
    .AddEnvironmentVariables()
    .Build();

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.File(configuration["Logging:Logpath"])
    .CreateLogger();

host.Run();
