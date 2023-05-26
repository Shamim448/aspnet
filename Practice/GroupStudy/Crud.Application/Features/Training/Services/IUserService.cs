using Crud.Domain.Entities;

namespace Crud.Application.Features.Training.Services
{
    public interface IUserService
    {
        public IList<User> GetAllUser();
        Task<(IList<User> records, int total, int totalDisplay)>
            GetPagedUserAsync(int pageIndex, int pageSize, string searchText, string orderBy);
    }
}
