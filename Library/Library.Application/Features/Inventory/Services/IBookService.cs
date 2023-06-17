using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Features.Inventory.Services
{
    public interface IBookService
    {
        //Create book
        void CreateBookItem(string name, double price, string type, string author);
        //view
        IList<Book> GetAllBook();
        

        //Ajax request
        Task<(IList<Book> records, int total, int totalDisplay)> 
            GetPagedBooksAsync(int pageIndex, int pageSize, string searchText, string orderBy);
        //update
        Book GetBook(Guid id);
        void UpdateBookItem(Guid id, string name, double price, string type, string author);
        //delete
        void DeleteBookItem(Guid id);
    }
}
