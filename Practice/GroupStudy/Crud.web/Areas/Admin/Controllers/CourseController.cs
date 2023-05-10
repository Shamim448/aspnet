using Autofac;
using Crud.web.Models;
using Microsoft.AspNetCore.Mvc;
using static System.Formats.Asn1.AsnWriter;

namespace Crud.web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CourseController : Controller
    {
        ILifetimeScope _scop;
        public CourseController(ILifetimeScope scope)
        {
            _scop = scope;
        }
        public IActionResult Index()
        {
            var model = _scop.Resolve<CourseModelList>();
            return View(model);
        }
        //collect data and send to ajex url
        

    }
}       
    
        
    

