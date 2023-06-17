using Library.Application.Features.Inventory.Repositories;
using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Library.Persistance.Features.Inventory.Repositories
{
    public class BookRepository : Repository<Book, Guid>, IBookRepository
    {
        public BookRepository(IApplicationDbContext dbContext) : base((DbContext)dbContext)
        {
        }
        //Ajex request
        
        public async Task<(IList<Book> records, int total, int totalDisplay)> 
            GetTableDataAsync(Expression<Func<Book, bool>> expression, string orderBy, int pageIndex, int pageSize)
        {
            return await GetDynamicAsync(expression, orderBy, null, pageIndex, pageSize, true);
        }

        public bool IsDuplicateBook(string name, Guid? id)
        {
            int? existingValue = null;
            if (id.HasValue)
                existingValue = GetCount(x => x.Name == name && x.Id != id.Value);
            else
                existingValue = GetCount(x => x.Name == name);
            return existingValue > 0;
            {
                
            }
        }

        
    }
}
