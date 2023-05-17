using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog.Events;
using Serilog;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using System.Reflection;
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
// Add services to the container.
try {
    //Autofac configuration Start
    builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
    builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
    {

    });
//Autofac configuration End
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
    //collect MigrationAssembly Path
    var migrationAssembly = Assembly.GetExecutingAssembly().FullName;
    //modify this method because applicationdbcontext has different project
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString, (x) => x.MigrationsAssembly(migrationAssembly)));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
    app.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
    app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
    Log.Information("Project Starting");
app.Run();
}
catch(Exception ex) {
    Log.Fatal(ex, "Application Terminated Unexpectedly");
}
finally {
Log.CloseAndFlush();
}
