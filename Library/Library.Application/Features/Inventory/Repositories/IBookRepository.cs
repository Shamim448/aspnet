using Library.Domain.Entities;
using Library.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Features.Inventory.Repositories
{
    public interface IBookRepository : IRepositoryBase<Book, Guid>
    {
        //Ajex request
        Task<(IList<Book> records, int total, int totalDisplay)> 
            GetTableDataAsync(Expression< Func<Book, bool>> expression, string orderBy, int pageIndex, int pageSize);
        bool IsDuplicateBook(string name, Guid? value);
    }
}
