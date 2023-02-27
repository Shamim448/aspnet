using Microsoft.AspNetCore.Mvc;
using MVCProject.Domain.Service;
using MVCProject.Web.Models;
using System.Diagnostics;

namespace MVCProject.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //Dependancy Injection by service controller
        private readonly ICourse _course;
        private readonly ICourseService _courseService;
        public HomeController(ILogger<HomeController> logger, ICourse course, ICourseService courseService)
        {
            _logger = logger;
            _course = course;
            _courseService = courseService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}