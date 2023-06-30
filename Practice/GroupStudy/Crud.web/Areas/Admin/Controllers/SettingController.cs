using Autofac;
using Crud.web.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;

namespace Crud.web.Areas.Admin.Controllers
{
    [Area("Admin")]
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
    }
}
