using Autofac;
using Crud.Infrastructure.Features.Exceptions;
using Crud.web.Areas.Admin.Models;
using DemoProject.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Crud.web.Areas.Admin
{
    [Area("Admin")]
    public class UserController : Controller
    {
        ILifetimeScope _scope;
        ILogger<UserController> _logger;
        public UserController(ILifetimeScope scope, ILogger<UserController> logger) 
        {
            _scope = scope;
            _logger = logger;
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
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(UserCreateModel model)
        {
            model.ResolveDependency(_scope);
            if (ModelState.IsValid)
            {
                
                try {
                    model.CreateUser();
                }
                catch (DuplicateNameException ex) {
                    _logger.LogError(ex, ex.Message);
                }
                catch (Exception c) {
                    _logger.LogError(c, "Server Error");
                }
            }
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
