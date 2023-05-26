using Crud.Application;
using Crud.Application.Features.Training.Services;
using Crud.Domain.Entities;

namespace Crud.Infrastructure.Features.Services
{
    public class UserService : IUserService
    {
        private readonly IApplicationUnitOfWork _unitOfWork;
        public UserService(IApplicationUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        //create user 
        public void CreateUser(string name, string email, string phone, string address)
        {
             
        }

        public IList<User> GetAllUser()
        {
            return _unitOfWork.Users.GetAll();
        }

        public async Task<(IList<User> records, int total, int totalDisplay)>
            GetPagedUserAsync(int pageIndex, int pageSize, string searchText, string orderBy)
        {
            return await Task.Run(() =>
            {
                var result = _unitOfWork.Users.GetDynamic(x => x.Name.Contains(searchText),
                orderBy, string.Empty, pageIndex, pageSize, true);
                return result;
            });
        }
    }
}
