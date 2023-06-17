using Autofac;
using AutoMapper;
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
        private IMapper _mapper;
        public UserUpdateModel()
        {

        }
        public UserUpdateModel(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
        internal void ResolveDependency(ILifetimeScope scope)
        {
            _userService = scope.Resolve<IUserService>();
            _mapper = scope.Resolve<IMapper>();
        }
        internal void Load(int id)
        {
            User user = _userService.GetUser(id);
            if(user != null)
            {
                _mapper.Map(user, this);
            }
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
