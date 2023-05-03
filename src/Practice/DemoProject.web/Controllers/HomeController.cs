using DemoProject.web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


namespace DemoProject.web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public IStudent _student;
        public HomeController(ILogger<HomeController> logger, IStudent student)
        {
            _logger = logger;
            _student = student;     
        }
        public IActionResult Index()
        {    
            _logger.LogError("This is a test error?");
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