 using Autofac;
using Crud.web.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Crud.web.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Roles ="Admin")]
    public class SettingController : Controller
    {
        private readonly ILifetimeScope _scope;
        private readonly ILogger<SettingController> _logger;
        public SettingController(ILogger<SettingController> logger, ILifetimeScope scope)
        {
            _logger = logger;
            _scope = scope;
        }
        public IActionResult Roles()
        {
            return View();
        }
        //create Role
        public async Task <IActionResult> CreateRole()
        {
            var model = _scope.Resolve<RoleCreateModel>();
            return View(model);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRole(RoleCreateModel model)
        {
            if (ModelState.IsValid)
            {
                model.ResolveDependency(_scope);
               await model.CreateRole();
            }
           return RedirectToAction(nameof(Roles));
        }

        //asign role
        public async Task<IActionResult> AssignRole()
        {
            var model = _scope.Resolve<RoleAssignModel>();
            await model.LoadData();
            return View(model);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignRole(RoleAssignModel model)
        {
            if (ModelState.IsValid)
            {
                model.ResolveDependency(_scope);
                await model.AssignRole();
            }
            return RedirectToAction(nameof(Roles));
        }

        //Assign Claim
        public async Task<IActionResult> AssignClaim()
        {
            var model = _scope.Resolve<RoleAssignModel>();
            await model.AsignStaticClaim();
            return View();
        }
    }
}
