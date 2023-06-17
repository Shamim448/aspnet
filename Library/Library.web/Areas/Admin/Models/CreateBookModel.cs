using Autofac;
using Library.Application.Features.Inventory.Services;
using Library.Infrastructure.Features.Services;
using System.ComponentModel.DataAnnotations;

namespace Library.web.Areas.Admin.Models
{
    public class CreateBookModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string Author { get; set; }
        public IBookService _bookService;
        public CreateBookModel()
        {

        }
        public CreateBookModel(IBookService bookService)
        {
            _bookService = bookService;
        }
        internal void ResolveDependency(ILifetimeScope scope)
        {
            _bookService = scope.Resolve<IBookService>();
        }

        internal void CreateBook()
        {
            if (!string.IsNullOrEmpty(Name)) {
                _bookService.CreateBookItem(Name, Price, Type, Author);
            }
        }
    }
}
