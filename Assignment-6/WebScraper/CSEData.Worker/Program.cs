using CSEData.Worker;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<DataService>();
        //services.AddHostedService<Worker>();
    })
    .Build();

host.Run();
