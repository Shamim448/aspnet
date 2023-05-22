using Autofac;
using Crud.web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Crud.web.Areas.Admin
{
    [Area("Admin")]
    public class UserController : Controller
    {
        ILifetimeScope _scope;
        public UserController(ILifetimeScope scope) 
        {
            _scope = scope;
        }
        public IActionResult Index()
        {
            var model = _scope.Resolve<UserListModel>();
            return View(model);
        }
    }
}
