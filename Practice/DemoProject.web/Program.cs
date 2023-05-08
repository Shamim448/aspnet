using Autofac;
using Autofac.Extensions.DependencyInjection;
using DemoProject.Application;
using DemoProject.Persistance;
using DemoProject.web;
using DemoProject.web.Data;
using DemoProject.web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
var migrationAssembly = Assembly.GetExecutingAssembly().FullName;

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new ApplicationModule(connectionString,
        migrationAssembly));
    containerBuilder.RegisterModule(new PersistenceModule(connectionString,
        migrationAssembly));
    containerBuilder.RegisterModule(new WebModule(connectionString,
        migrationAssembly));
});


// Add services to the container.

builder.Services.AddDbContext< ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString, (x) => x.MigrationsAssembly(migrationAssembly)));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();
//D.I by service collection
//builder.Services.AddTransient<IStudent, Student>();
//builder.Services.AddSingleton<IStudent, Student>();
//builder.Services.AddScoped<IStudent, Student>();

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

app.Run();
