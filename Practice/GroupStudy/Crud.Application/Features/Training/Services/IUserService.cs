using Crud.Domain.Entities;

namespace Crud.Application.Features.Training.Services
{
    public interface IUserService
    {
        //For Create
        void CreateUser(string name, string email, string phone, string address);
        //For Update
        User GetUser(Guid id);
        void UpdateUser(Guid id, string name, string email, string phone, string address);
        //Delete
        void DeleteUser(Guid id);
        //View Data Table
        public IList<User> GetAllUser();
        Task<(IList<User> records, int total, int totalDisplay)>
            GetPagedUserAsync(int pageIndex, int pageSize, string searchText, string orderBy);
        
    }
}
