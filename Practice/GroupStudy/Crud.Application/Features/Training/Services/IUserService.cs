using Crud.Domain.Entities;

namespace Crud.Application.Features.Training.Services
{
    public interface IUserService
    {
        //For Create
        void CreateUser(string name, string email, string phone, string address);
        //For Update
        User GetUser(int id);
        void UpdateUser(int id, string name, string email, string phone, string address);
        //View Data Table
        public IList<User> GetAllUser();
        Task<(IList<User> records, int total, int totalDisplay)>
            GetPagedUserAsync(int pageIndex, int pageSize, string searchText, string orderBy);
        
    }
}
