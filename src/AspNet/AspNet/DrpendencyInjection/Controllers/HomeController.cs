using DrpendencyInjection.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DrpendencyInjection.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICourse _course;
        public HomeController(ILogger<HomeController> logger, ICourse course)
        {
            _logger = logger;
            _course = course;
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