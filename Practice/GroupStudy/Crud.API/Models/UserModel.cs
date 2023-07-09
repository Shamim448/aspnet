using Autofac;
using AutoMapper;
using Crud.Application.Features.Training.Services;
using Crud.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Crud.API.Models
{
    public class UserModel
    {
        private IUserService? _userService;
        private IMapper _mapper;

        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }

        public UserModel()
        {

        }
        public UserModel(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
        public void ResolveDepenency(ILifetimeScope scope)
        {
            _userService = scope.Resolve<IUserService>();
            _mapper = scope.Resolve<IMapper>();
        }

        internal IList<User> GetUsers()
        {
            return _userService?.GetAllUser();
        }
        internal void DeleteUser(Guid id)
        {
            _userService.DeleteUser(id);
        }
        internal void CreateUser()
        {
            User user = _mapper.Map<User>(this);
            _userService.CreateUser(user);
        }
        internal void UpdateUser()
        {
            User user = _mapper.Map<User>(this);
            _userService.EditUser(user);
        }
        internal User GetUser(string name)
        {
            return _userService.GetUser(name);
        }
        internal User GetUser(Guid id)
        {
            return _userService.GetUser(id);
        }
    }
}
