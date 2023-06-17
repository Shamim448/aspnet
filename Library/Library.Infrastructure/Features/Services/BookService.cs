using Library.Application;
using Library.Application.Features.Inventory.Services;
using Library.Domain.Entities;
using Library.Infrastructure.Features.Exceptions;

namespace Library.Infrastructure.Features.Services
{
    public class BookService : IBookService
    {
        private readonly IApplicationUnitOfWork _unitOfWork;
        public BookService(IApplicationUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }
        //Create Book
        public void CreateBookItem(string name, double price, string type, string author)
        {
            if(_unitOfWork.Books.IsDuplicateBook(name, null))
            {
                throw new DuplicateNameException("Book Name is duplicate");
            }
            Book book = new Book()
            {
                Name = name,
                Price = price,
                Type = type,
                Author = author
            };
            _unitOfWork.Books.Add(book);
            _unitOfWork.Save();
        }

        //data view
        public IList<Book> GetAllBook()
        {
            return _unitOfWork.Books.GetAll();
        }
       

        //ajax request
        public async Task<(IList<Book> records, int total, int totalDisplay)> 
            GetPagedBooksAsync(int pageIndex, int pageSize, string searchText, string orderBy)
        {
            var result = await _unitOfWork.Books.GetTableDataAsync(
                x => x.Name.Contains(searchText) || x.Author.Contains(searchText), orderBy, pageIndex, pageSize);
            return result;
        }
        //Update
        public Book GetBook(Guid id)
        {
            return _unitOfWork.Books.GetById(id);
        }
        public void UpdateBookItem(Guid id, string name, double price, string type, string author)
        {
            if(_unitOfWork.Books.IsDuplicateBook(name, id))
            {
                throw new DuplicateNameException("Book Name or Id Doplicate");
            }
            Book book = _unitOfWork.Books.GetById(id); 
            book.Name = name;
            book.Price = price;
            book.Type = type;
            book.Author = author;
            _unitOfWork.Save();
        }

        public void DeleteBookItem(Guid id)
        {
            _unitOfWork.Books.Remove(id);
            _unitOfWork.Save();
        }
    }
}
