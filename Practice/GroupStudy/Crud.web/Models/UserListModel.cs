using Crud.Domain.Entities;
using Crud.Domain.Services;

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
    }
}
