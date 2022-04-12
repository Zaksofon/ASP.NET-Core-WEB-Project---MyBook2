using System.Collections.Generic;
using MyBook2.Models.Home;

namespace MyBook2.Services.Books
{
    public interface IBookService
    {
        BookQueryServiceModel All(string author, string searchTerm, int currentPage, int booksPerPage);

        IEnumerable<LatestBookServiceModel> Latest();

        BookDetailsServiceModel Details(int bookId);

        int Create(
            string title, 
            string author, 
            string description, 
            string imageUrl, 
            int genreId, 
            int issueYear,
            int librarianId);

        bool Edit(int bookId,
            string title,
            string author,
            string description,
            string imageUrl,
            int genreId,
            int issueYear);

        IEnumerable<BookServiceModel> ByUser(string userId);

        IEnumerable<string> AllAuthors();

        bool IsByLibrarian(int bookId, int librarianId);

        IEnumerable<BookGenreServiceModel> AllGenres();

        bool GenreExists(int genreId);
    }
}
