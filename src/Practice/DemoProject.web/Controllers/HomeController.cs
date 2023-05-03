using DemoProject.Application.Services;
using DemoProject.Domain.Services;
using DemoProject.web.Data;
using DemoProject.web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


namespace DemoProject.web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IStudentService _studentService;
        private readonly IConfiguration _config;
        public HomeController(ILogger<HomeController> logger, IStudentService studentService, IConfiguration config)
        {
            _logger = logger;
            _studentService = studentService;
            _config = config;
        }
        public IActionResult Index()
        {    
            AdoNetUtility adoNetUtility = new AdoNetUtility(_config.GetConnectionString("DefaultConnection"));
            adoNetUtility.WriteOperation("Test query"); 
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