using System.Collections.Generic;

namespace MyBook2.Services.Books
{
    public interface IBookService
    {
        BookQueryServiceModel All(string author, string searchTerm, int currentPage, int booksPerPage);

        IEnumerable<string> AllBooksAuthors();
    }
}
