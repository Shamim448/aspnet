using Autofac;
using Crud.API.Models;
using Crud.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Crud.API.Controllers
{
    [Route("v3/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILifetimeScope _scope;
        private readonly ILogger<UsersController> _logger;

        public UsersController(ILifetimeScope scope, ILogger<UsersController> logger)
        {
            _scope = scope;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<User> Get() { 
            try
            {
                var model = _scope.Resolve<UserModel>();
                return model.GetUsers();
            }
            catch(Exception ex) {
                _logger.LogError(ex, "Couldn't get courses");
                return null;
            }
        }
        [HttpGet("{id}")]
        public User Get(Guid id) {
            var model = _scope.Resolve<UserModel>();
            return model.GetUsers(id);
        }
        [HttpGet("{name}")]
        public User Get(string name)
        {
            var model = _scope.Resolve<UserModel>();
            return model.GetUsers(name);
        }

        [HttpPost()]
        public IActionResult Post(UserModel model)
        {
            try {
                model.ResolveDepenency(_scope);
                model.CreateUser();
                return Ok();
            }
            catch (Exception ex) {
                _logger.LogError(ex, "Couldn't Updete user");
                return BadRequest();
            }
        }
        [HttpPut]
        public IActionResult Put(UserModel model)
        {
            try
            {
                model.ResolveDepenency(_scope);
                model.UpdateUser();
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Couldn't Updete user");
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                var model = _scope.Resolve<UserModel>();
                model.DeleteUser(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Couldn't delete user");
                return BadRequest();
            }
        }
    }
}
