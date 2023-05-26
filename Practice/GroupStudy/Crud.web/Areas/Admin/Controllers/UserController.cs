using Autofac;
using Crud.web.Areas.Admin.Models;
using DemoProject.Infrastructure;
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
        public IActionResult Create() 
        {
            var model = _scope.Resolve<UserCreateModel>();
            return View(model);
        }
        public async Task <JsonResult> GetUsers()
        {
            var dataTableModel = new DataTablesAjaxRequestUtility(Request);
            var model = _scope.Resolve<UserListModel>();
            var data = await model.GetPagedUsers(dataTableModel);
            return Json(data);
        }
    }
}
