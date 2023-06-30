using Autofac;
using Crud.Persistance.Features.Membership;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Crud.web.Areas.Admin.Models
{
    public class RoleCreateModel
    {
        [Required]
        public string Name { get; set; }

        private RoleManager<ApplicationRole> _roleManager;
        private UserManager<ApplicationUser> _userManager;
        
        public RoleCreateModel() { 

        }
        public RoleCreateModel(RoleManager<ApplicationRole> roleManager,
            UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        internal void ResolveDependency (ILifetimeScope scope)
        {
            _roleManager = scope.Resolve<RoleManager<ApplicationRole>>();
            _userManager = scope.Resolve<UserManager<ApplicationUser>>();
        }

        public async Task  CreateRole()
        {
            if(!string.IsNullOrWhiteSpace(Name))
            {
                await _roleManager.CreateAsync(new ApplicationRole(Name));
            }
        }
    }
}
