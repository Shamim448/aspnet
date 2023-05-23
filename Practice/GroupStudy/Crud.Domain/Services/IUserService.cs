using Crud.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud.Domain.Services
{
    public interface IUserService
    {
        public IList<User> GetAllUser();
        Task<(IList<User> records, int total, int totalDisplay)>
            GetPagedUserAsync(int pageIndex, int pageSize, string searchText, string orderBy);
    }
}
