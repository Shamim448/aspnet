using Autofac.Extensions.DependencyInjection;
using Autofac;
using Crud.Application;
using Crud.Infrastructure;
using Serilog;
using Serilog.Events;
using System.Reflection;
using Crud.API.Models;
using Crud.Persistance;

var builder = WebApplication.CreateBuilder(args);
//serilog Configure
builder.Host.UseSerilog((hc, lc) => lc //hc== hosting context lc= loging context
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
  .ReadFrom.Configuration(builder.Configuration)
  );
//End Serilog config
try
{
    // Add services to the container.
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
    //collect MigrationAssembly Path
    var migrationAssembly = Assembly.GetExecutingAssembly().FullName;

    //Autofac configuration Start
    builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
    builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
    {
        containerBuilder.RegisterModule(new PersistanceModule(connectionString, migrationAssembly));
        containerBuilder.RegisterModule(new ApplicationModule());
        containerBuilder.RegisterModule(new InfrastructureModule());
        containerBuilder.RegisterModule(new ApiModule());

    });
    //Autofac configuration End
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();
    //Auto Mapper
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();
    Log.Information("Project Starting");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application Terminated Unexpectedly");
}
finally
{
    Log.Information("Project Starting");
}
