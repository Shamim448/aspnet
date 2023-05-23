using Crud.Domain.Entities;
using Crud.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IApplicationUnitOfWork _unitOfWork;
        public UserService(IApplicationUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IList<User> GetAllUser()
        {  
            return _unitOfWork.Users.GetAll();
        }

        public async Task<(IList<User> records, int total, int totalDisplay)> GetPagedUserAsync(int pageIndex, int pageSize, string searchText, string orderBy)
        {
            return await Task.Run(() => {
                var result = _unitOfWork.Users.GetDynamic(x => x.Name.Contains(searchText),
                orderBy, string.Empty, pageIndex, pageSize, true);

                return result;
            });
        }
    }
}
