using Autofac;
using Crud.Persistance.Features.Membership;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Crud.web.Areas.Admin.Models
{
    public class CreateRoleModel
    {
        [Required]
        public string UserName { get; set; }

        private RoleManager<ApplicationUserRole> _roleManager;
        private UserManager<ApplicationUser> _userManager;
        
        public CreateRoleModel() { 

        }
        public CreateRoleModel(RoleManager<ApplicationUserRole> roleManager,
            UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        internal void ResolveDependency (ILifetimeScope scope)
        {
            _roleManager = scope.Resolve<RoleManager<ApplicationUserRole>>();
            _userManager = scope.Resolve<UserManager<ApplicationUser>>();
        }




    }
}
