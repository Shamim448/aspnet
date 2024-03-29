﻿using Autofac;
using AutoMapper;
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

        private  IUserService _userService;
        private  IMapper _mapper;
        public UserCreateModel() 
        { 
        
        }
        public UserCreateModel(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
        internal void ResolveDependency(ILifetimeScope scope)
        {
            _userService = scope.Resolve<IUserService>();
        }
        internal void CreateUser()
        {
            if (!string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Email))
            {
                _userService.CreateUser(Name, Email, Phone, Address);
            }
        }
    }
}
