using Autofac;
using Crud.Persistance.Features.Membership;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Crud.web.Areas.Admin.Models
{
    public class RoleAssignModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string RoleName { get; set; }
        public List<SelectListItem>? Roles { get;private set; }
        public List<SelectListItem>? LUsers { get; private set; }

        private RoleManager<ApplicationRole> _roleManager;
        private UserManager<ApplicationUser> _userManager;
        
        public RoleAssignModel() { 

        }
        public RoleAssignModel(RoleManager<ApplicationRole> roleManager, 
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
        //used for Load Username and role name in vied page
        internal async Task LoadData()
        {
            LUsers = await (from c in _userManager.Users
                           select new SelectListItem($"{c.FirstName} {c.LastName}", c.UserName))
            .ToListAsync();

            Roles = await (from c in _roleManager.Roles
                           select new SelectListItem(c.Name, c.Name))
                     .ToListAsync();
        }
        //assign role
        internal async Task AssignRole()
        {
            ApplicationUser user = await _userManager.FindByNameAsync(Username);
            await _userManager.AddToRoleAsync(user, RoleName);   
        }

    }
}
