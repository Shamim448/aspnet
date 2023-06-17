using Autofac;
using Library.Infrastructure;
using Library.Infrastructure.Features.Exceptions;
using Library.web.Areas.Admin.Models;
using Library.web.Models;
using Library.web.Utilities;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Library.web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BookController : Controller
    {
        ILifetimeScope _scope;
        ILogger<BookController> _logger;
        public BookController(ILifetimeScope scope, ILogger<BookController> logger)
        {
            _scope = scope;
            _logger = logger;
        }
        //Data view fron Database
        public IActionResult Index()
        {
            var model = _scope.Resolve<BookListModel>();
            return View(model);
        }
        //Ajax request handeller
        public async Task<JsonResult> GetBooks()
        {
            var dataTableUtility = new DatatableAjaxRequestUtility(Request);
            var model = _scope.Resolve<BookListModel>();
            var data = await model.GetPagedBooksAsync(dataTableUtility);
            return Json(data);
        }
        //Create
        public IActionResult Create()
        {
            var model = _scope.Resolve<CreateBookModel>();
            return View(model);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(CreateBookModel model)
        {
            model.ResolveDependency(_scope);
            if(ModelState.IsValid)
            {
                try
                {
                    model.CreateBook();
                    TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                    {
                        Message = "Successfully Insert a Book.",
                        Type = ResponseType.Success
                    });
                    return RedirectToAction("Index");
                }
                catch(DuplicateNameException ex)
                {
                    _logger.LogError(ex, ex.Message);
                    TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                    {
                        Message = ex.Message,
                        Type = ResponseType.Danger
                    });
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Server Error");
                    TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                    {
                        Message = "Server Error to insert Book",
                        Type = ResponseType.Danger
                    });
                }
                
            }
            return View(model);
        }
        //Update
        public IActionResult Update(Guid id)
        {
            var model = _scope.Resolve<UpdateBookModel>();
            model.Load(id);
            return View(model);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Update(UpdateBookModel model)
        {
            model.ResolveDependency(_scope);
            if (ModelState.IsValid)
            {
                try
                {
                    model.UpdateBook();
                    TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                    {
                        Message = "Successfully Updatw a Book.",
                        Type = ResponseType.Success
                    });
                    return RedirectToAction("Index");
                }
                catch (DuplicateNameException ex)
                {
                    _logger.LogError(ex, ex.Message);
                    TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                    {
                        Message = ex.Message,
                        Type = ResponseType.Danger
                    });
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Server Error");
                    TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                    {
                        Message = "Server Error to Update Book",
                        Type = ResponseType.Danger
                    });
                }

            }
            return View(model);
        }
        //delete
        //Delete
        public IActionResult Delete(Guid id)
        {
            var model = _scope.Resolve<BookListModel>();
            if (ModelState.IsValid)
            {
                try
                {
                    model.DeleteBook(id);
                    TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                    {
                        Message = "Delete Successfull",
                        Type = ResponseType.Success
                    });
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Server Error");
                }
            }
            return RedirectToAction("Index");
        }
    }
}
