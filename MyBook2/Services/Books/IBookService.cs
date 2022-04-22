using System.Collections.Generic;
using MyBook2.Models.Home;
using MyBook2.Services.Books.Models;

namespace MyBook2.Services.Books
{
    public interface IBookService
    {
        BookQueryServiceModel All(
            string author = null, 
            string searchTerm = null,
            int currentPage = 1,
            int booksPerPage = int.MaxValue,
            bool publicOnly = true);

        IEnumerable<LatestBookServiceModel> Latest();

        BookDetailsServiceModel Details(int bookId);

        int Create(
            string title, 
            string author, 
            string description, 
            string imageUrl,
            string filePDF,
            int genreId, 
            int issueYear,
            int librarianId);

        bool Edit(int bookId,
            string title,
            string author,
            string description,
            string imageUrl,
            string filePDF,
            int genreId,
            int issueYear,
            bool isPublic);

        IEnumerable<BookServiceModel> ByUser(string userId);

        bool IsByLibrarian(int bookId, int librarianId);

        void Change(int bookId);
        void Delete(int bookId);

        IEnumerable<string> AllAuthors();

        IEnumerable<BookGenreServiceModel> AllGenres();

        bool GenreExists(int genreId);
    }
}
