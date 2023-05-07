using Autofac;
using DemoProject.Domain.Entities;
using DemoProject.Infrastructure;
using DemoProject.web.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemoProject.web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StudentController : Controller
    {
        ILifetimeScope _scope;
        public StudentController(ILifetimeScope scope)
        {
            _scope = scope;
        }
        public IActionResult Index()
        {
           var model = _scope.Resolve<StudentListModel>();
            return View(model);
        }
        //collect data and send to ajex url
        public async Task <JsonResult> GetStudents()
        {
            var dataTableModel =new DataTablesAjaxRequestUtility(Request);
            var model = _scope.Resolve<StudentListModel>();
            var data = await model.GetPagedStudents(dataTableModel);
            return Json(data);
        }
         
    }
}
