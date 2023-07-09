using Autofac;
using AutoMapper;
using Crud.Application.Features.Training.Services;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Crud.API.Models
{
    public class UserModel
    {
        private IUserService? _userService;
        private IMapper _mapper;

        public int Id { get; set; }
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
    }
}
