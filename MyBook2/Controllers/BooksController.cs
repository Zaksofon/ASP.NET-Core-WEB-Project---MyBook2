using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBook2.Data;
using MyBook2.Data.Models;
using MyBook2.Infrastructure;
using MyBook2.Models.Book;
using System.Collections.Generic;
using System.Linq;
using MyBook2.Services.Books;

namespace MyBook2.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookService books;
        private readonly MyBook2DbContext data;

        public BooksController(MyBook2DbContext data, IBookService books)
        {
            this.data = data;
            this.books = books;
        }

        //AllBooksQueryModel instead of (string author, string searchTerm, AllBooksSorting sorting) in the Method below!
        public IActionResult All([FromQuery]AllBooksQueryModel query)
        {
            var queryResult = this.books
                .All(query.Author, query.SearchTerm, query.CurrentPage, AllBooksQueryModel.BooksPerPage);

            var bookAuthors = this.books.AllBooksAuthors();

            query.TotalBooks = queryResult.TotalBooks;
            query.Authors = bookAuthors;
            query.Books = queryResult.Books;

            return View(query);
        }

        [Authorize]
        public IActionResult Add()
        {
            if (!UserIsLibrarian())
            {
                return RedirectToAction(nameof(LibrariansController.Become), "Librarians");
            }

            return View(new AddBookFormModel
            {
                Genres = GetBookGenre()
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(AddBookFormModel book)
        {
            var librarianId = data
                .Librarians
                .Where(l => l.UserId == User.GetId())
                .Select(l => l.Id)
                .FirstOrDefault();

            if (librarianId == 0)
            {
                return RedirectToAction(nameof(LibrariansController.Become), "Librarians");
            }

            if (!data.Genres.Any(g => g.Id == book.GenreId))
            {
                ModelState.AddModelError(nameof(book.GenreId), "Genre does not exist.");
            }

            if (!ModelState.IsValid)
            {
                book.Genres = GetBookGenre();

                return View(book);
            }

            var bookLibrary = new Book
            {
                Title = book.Title,
                Author = book.Author,
                Description = book.Description,
                ImageUrl = book.ImageUrl,
                GenreId = book.GenreId,
                IssueYear = book.IssueYear,
                LibrarianId = librarianId
            };

            data.Books.Add(bookLibrary);

            data.SaveChanges(); 

            return RedirectToAction(nameof(All));
        }

        private IEnumerable<BookGenreViewModel> GetBookGenre() => data
                .Genres
                .Select(g => new BookGenreViewModel()
                {
                    Id = g.Id,
                    Name = g.Name
                })
                .ToList();

        private bool UserIsLibrarian()
        {
            var userId = User.GetId();

            return this.data
                .Librarians
                .Any(l => l.UserId == userId);
        }
    }
}
