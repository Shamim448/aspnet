using Crud.Infrastructure.Securities;
using Crud.Persistance.Features.Membership;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic; 
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud.Persistance.Extentions
{
    public static class ServiceCollectionExtentions
    {
        public static void AddIdentity(this IServiceCollection services)
        {
            services
                .AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddUserManager<ApplicationUserManager>()
                .AddRoleManager<ApplicationRoleManager>()
                .AddSignInManager<ApplicationSignInManager>()
                .AddDefaultTokenProviders();

            

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
            });

            //Role Management
            services.AddAuthorization(options =>
            {
                //Policy Based
                options.AddPolicy("ITPerson", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireRole("HR");
                    policy.RequireRole("IT");
                });
                //Claim Based
                options.AddPolicy("UserViewPolicy", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("ViewUser", "true");
                });
                //Alternative option for Claim Based
                options.AddPolicy("UserViewRequirementPolicy", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.Requirements.Add(new UserViewRequirement());
                });
            });
            //part of Alternative option for Claim Based
            services.AddSingleton<IAuthorizationHandler, UserViewRequirementHandler>();

            services.AddRazorPages();
        }
    }
}
