﻿using Microsoft.AspNetCore.Mvc;
using MVCProject.Domain.Service;
using MVCProject.Web.Models;
using System.Diagnostics;

namespace MVCProject.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //Dependancy Injection by service controller
                private readonly ICourseService _courseService;
        public HomeController(ILogger<HomeController> logger, ICourseService courseService)
        {
            _logger = logger;
            //_course = course;
            _courseService = courseService;
        }
        public IActionResult Index()
        {
            _logger.LogInformation("I am Index Page");
            return View();
        }
        //Html and Tag Helper
        public IActionResult Test()
        {
            var model = new TestModel();
            return View(model);
        }
        //used for recive post data
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Test(TestModel model)
        {
            return View(model);
        }
        //Tag helper view page
        public IActionResult AnotherTest()
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