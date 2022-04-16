using System.Collections.Generic;

namespace MyBook2.Services.Books.Models
{
    public class BookQueryServiceModel
    {
        public int CurrentPage { get; init; }

        public int BooksPerPage { get; init; }

        public int TotalBooks { get; init; }

        public IEnumerable<BookServiceModel> Books { get; init; }
    }
}
