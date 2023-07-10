using Autofac;
using AutoMapper;
using Crud.Application.Features.Training.Services;
using Crud.Domain.Entities;
using DemoProject.Infrastructure;
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

        internal IList<User>? GetUsers()
        {
            return _userService?.GetAllUser();
        }
        internal void DeleteUser(Guid id)
        {
            _userService?.DeleteUser(id);
        }
        internal void CreateUser()
        {   
            _userService?.CreateUser(Name, Email, Phone, Address);
        }
        internal void UpdateUser()
        { 
            _userService?.UpdateUser(Id, Name, Email, Phone, Address);
        }
        //internal User GetUser(string name)
        //{
        //    return _userService.GetUser(name);
        //}
        internal User? GetUser(Guid id)
        {
            return _userService?.GetUser(id);
        }

        public async Task<object?> GetPagedUsers(DataTablesAjaxRequestUtility dataTablesUtility)
        {
            var data = await _userService? .GetPagedUserAsync(
               dataTablesUtility.PageIndex,
               dataTablesUtility.PageSize,
               dataTablesUtility.SearchText,
               dataTablesUtility.GetSortText(new string[] { "Id", "Name", "Email", "Phone", "Address" }));

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                                record.Name,
                                record.Email,
                                record.Phone,
                                record.Address,
                                record.Id.ToString(),

                        }
                    ).ToArray()
            };
        } 
    }
}
