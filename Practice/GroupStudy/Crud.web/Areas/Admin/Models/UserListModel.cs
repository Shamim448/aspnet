using Crud.Application.Features.Training.Services;
using Crud.Domain.Entities;
using DemoProject.Infrastructure;

namespace Crud.web.Areas.Admin.Models
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

        internal void DeleteUser(Guid id)
        {
            _userService.DeleteUser(id);
        }
    }
}
