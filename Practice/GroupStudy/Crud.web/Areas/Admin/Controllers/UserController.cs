using Autofac;
using Crud.Infrastructure.Features.Exceptions;
using Crud.web.Areas.Admin.Models;
using Crud.Web.Models;
using Crud.Web.Utilities;
using DemoProject.Infrastructure;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Policy = "UserViewRequirementPolicy")]
        public IActionResult Index()
        {
            var model = _scope.Resolve<UserListModel>();
            return View(model); 
        }
        //-------Used for create--------
        [Authorize(Policy = "ITPerson")]
        public IActionResult Create() 
        {
            var model = _scope.Resolve<UserCreateModel>();
            return View(model);
        }
        [HttpPost, ValidateAntiForgeryToken,  Authorize(Policy = "ITPerson")]
        public IActionResult Create(UserCreateModel model)
        {
            model.ResolveDependency(_scope);
            if (ModelState.IsValid)
            {  
                try {
                    model.CreateUser();
                    TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                    {
                        Message = "Successfully added a new User.",
                        Type = ResponseTypes.Success
                    });
                    return RedirectToAction("Index");
                }
                catch (DuplicateNameException ex) {
                    _logger.LogError(ex, ex.Message);
                    TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                    {
                        Message = ex.Message,
                        Type = ResponseTypes.Danger
                    });
                }
                catch (Exception e) {
                    _logger.LogError(e, "Server Error");
                    TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                    {
                        Message = "There was a problem in creating course.",
                        Type = ResponseTypes.Danger
                    });
                }
            }
            return View(model);
        }
        //-------End section for create--------
        //-------Used for Update--------
        [Authorize(Policy = "ITPerson")]
        public IActionResult Update(Guid id)
        {
            var model = _scope.Resolve<UserUpdateModel>();
            model.Load(id);
            return View(model);
        }
        [HttpPost, ValidateAntiForgeryToken,  Authorize(Policy = "ITPerson")]
        public IActionResult Update(UserUpdateModel model)
        {
            model.ResolveDependency(_scope);
            if (ModelState.IsValid)
            {
                try
                {
                    model.UpdateUser();
                    TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                    {
                        Message = "Successfully Update a selected user.",
                        Type = ResponseTypes.Success
                    });
                    return RedirectToAction("Index");
                }
                catch (DuplicateNameException ex)
                {
                    _logger.LogError(ex, ex.Message);
                    TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                    {
                        Message = ex.Message,
                        Type = ResponseTypes.Danger
                    });
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Server Error");
                    TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                    {
                        Message = "There was a problem in Updating user.",
                        Type = ResponseTypes.Danger
                    });
                }
            }
            return View(model);
        }
        //-------End section for Update--------
        //-------Start section for Delete--------
        [Authorize(Policy = "ITPerson")]
        public IActionResult Delete(Guid id)
        {
            var model = _scope.Resolve<UserListModel>();
            if (ModelState.IsValid)
            {
                try
                {
                    model.DeleteUser(id);
                }
                
                catch (Exception e)
                {
                    _logger.LogError(e, "Server Error");
                }
            }
            return RedirectToAction("Index");
        }
        [Authorize(Policy = "UserViewRequirementPolicy")]
        public async Task <JsonResult> GetUsers()
        {
            var dataTableModel = new DataTablesAjaxRequestUtility(Request);
            var model = _scope.Resolve<UserListModel>();
            var data = await model.GetPagedUsers(dataTableModel);
            return Json(data);
        }
    }
}
