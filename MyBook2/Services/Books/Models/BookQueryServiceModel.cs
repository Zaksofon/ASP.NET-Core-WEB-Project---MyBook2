using System.Collections.Generic;
using MyBook2.Services.Books;
using MyBook2.Services.Books.Models;

namespace MyBook2.Services
{
    public class BookQueryServiceModel
    {
        public int CurrentPage { get; init; }

        public int BooksPerPage { get; init; }

        public int TotalBooks { get; init; }

        public IEnumerable<BookServiceModel> Books { get; init; }
    }
}
