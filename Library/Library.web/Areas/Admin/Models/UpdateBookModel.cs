using Autofac;
using Library.Application.Features.Inventory.Services;
using Library.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Library.web.Areas.Admin.Models
{
    public class UpdateBookModel
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string Author { get; set; }
        public IBookService _bookService;
        public UpdateBookModel()
        {

        }
        public UpdateBookModel(IBookService bookService)
        {
            _bookService = bookService;
        }
        internal void ResolveDependency(ILifetimeScope scope)
        {
            _bookService = scope.Resolve<IBookService>();
        }

        internal void UpdateBook()
        {
            if(!string.IsNullOrEmpty(Name))
            {
                _bookService.UpdateBookItem(Id, Name, Price, Type, Author);
            }
        }
        //used for input field esisting data load
        internal void Load(Guid id)
        {
            Book book = _bookService.GetBook(id);
            Id = book.Id;
            Name = book.Name; 
            Price = book.Price;
            Type = book.Type;
            Author = book.Author;
        }
    }
}
