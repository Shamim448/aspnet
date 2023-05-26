using Crud.Application.Features.Training.Services;
using System.ComponentModel.DataAnnotations;

namespace Crud.web.Areas.Admin.Models
{
    public class UserCreateModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Address { get; set; }
        private readonly IUserService _userService;
        public UserCreateModel() { }
        public UserCreateModel(IUserService userService)
        {
            _userService = userService;
        }
    }
}
