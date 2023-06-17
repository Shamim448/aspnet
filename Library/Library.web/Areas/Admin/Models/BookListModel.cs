using Library.Application.Features.Inventory.Services;
using Library.Domain.Entities;
using Library.Infrastructure;

namespace Library.web.Areas.Admin.Models
{
    public class BookListModel
    {
        private readonly IBookService _bookService;
        public BookListModel()
        {

        }
        public BookListModel(IBookService bookService)
        {
            _bookService = bookService;
        }
        //Data view
        public IList<Book> GetBooks()
        {
            return _bookService.GetAllBook();
        }
        //Ajax request handel
        public async Task<object> GetPagedBooksAsync(DatatableAjaxRequestUtility dataTableUtility)
        {
            var data = await _bookService.GetPagedBooksAsync(
                dataTableUtility.PageIndex,
                dataTableUtility.PageSize,
                dataTableUtility.SearchText,
                dataTableUtility.GetSortText(new string[] { "Name", "Price", "Type", "Author" })
                );
                return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                            record.Name,
                            record.Price.ToString(),
                            record.Type,
                            record.Author,
                            record.Id.ToString()

                        }).ToArray()
            };
        }

        internal void DeleteBook(Guid id)
        {
            _bookService.DeleteBookItem(id);
        }
    }
}
