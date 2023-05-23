﻿using Crud.Domain.Entities;
using Crud.Domain.Services;
using DemoProject.Infrastructure;

namespace Crud.web.Models
{
    public class UserListModel
    {
        private readonly IUserService _userService;
        public UserListModel()
        {

        }
        public UserListModel(IUserService userService)
        {
            _userService = userService;
        }

        public IList<User> GetUsers()
        {
            return _userService.GetAllUser();
        }

        public async Task<object> GetPagedUsers(DataTablesAjaxRequestUtility dataTablesUtility)
        {
            var data = await _userService.GetPagedUserAsync(
               dataTablesUtility.PageIndex,
               dataTablesUtility.PageSize,
               dataTablesUtility.SearchText,
               dataTablesUtility.GetSortText(new string[] { "Name", "Fee" }));

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                                record.Id.ToString(), 
                                record.Name,
                                record.Email,
                                record.Phone,
                                record.Address,
                                
                        }
                    ).ToArray()
            };
        }
    }
}
