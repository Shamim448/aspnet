using Autofac;
using Crud.API.Models;
using Crud.Domain.Entities;
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
    }
}
