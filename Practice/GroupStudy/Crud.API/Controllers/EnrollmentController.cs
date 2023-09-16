using Autofac;
using Crud.API.Models;
using Crud.Domain.Entities;
using DemoProject.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Crud.API.Controllers
{
    [Route("v3/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly ILifetimeScope _scope;
        private readonly ILogger<UsersController> _logger;

        public EnrollmentController(ILifetimeScope scope, ILogger<UsersController> logger)
        {
            _scope = scope;
            _logger = logger;
        }

        [HttpGet, Authorize]
        public async Task<object> Get()
        {
            var model = _scope.Resolve<EnrollmentModel>();
            model.SearchItem = new EnrollmentSearch();
            model.SearchItem.CourseName = Request.Query["SearchItem[CourseName]"];
            model.SearchItem.UserName = Request.Query["SearchItem[UserName ]"];

            model.SearchItem.enrollmentDateFrom = Request.Query["SearchItem[EnrollmentDateFrom]"];
            model.SearchItem.enrollmentDateTo = Request.Query["SearchItem[EnrollmentDateTo]"];

            //if (!string.IsNullOrWhiteSpace(enrollmentDateFrom))
            //    model.SearchItem.EnrollmentDateFrom = DateTime.Parse(enrollmentDateFrom);
            //else
            //    model.SearchItem.EnrollmentDateFrom = null;

            //if (!string.IsNullOrWhiteSpace(enrollmentDateTo))
            //    model.SearchItem.EnrollmentDateTo = DateTime.Parse(enrollmentDateTo);
            //else
            //    model.SearchItem.EnrollmentDateTo = null;

            var dataTablesModel = new DataTablesAjaxRequestUtility(Request);
            model.ResolveDependency(_scope);

            var data = await model.GetPagedCoursesAdvanced(dataTablesModel);

            return data;
        }
    }
}
