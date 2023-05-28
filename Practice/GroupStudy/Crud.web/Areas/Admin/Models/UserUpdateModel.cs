using Autofac;
using Crud.Application.Features.Training.Services;
using Crud.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Crud.web.Areas.Admin.Models
{
    public class UserUpdateModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Address { get; set; }

        private IUserService _userService;
        public UserUpdateModel()
        {

        }
        public UserUpdateModel(IUserService userService)
        {
            _userService = userService;
        }
        internal void ResolveDependency(ILifetimeScope scope)
        {
            _userService = scope.Resolve<IUserService>();
        }
        internal void Load(int id)
        {
            User user = _userService.GetUser(id);
            Id = user.Id;
            Name = user.Name;
            Email = user.Email;
            Phone = user.Phone;
            Address = user.Address;
        }
        internal void UpdateUser()
        {
            if (!string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Email))
            {
                _userService.UpdateUser(Id, Name, Email, Phone, Address);
            }
        }
    }
}
