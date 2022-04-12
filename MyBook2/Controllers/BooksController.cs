using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBook2.Infrastructure;
using MyBook2.Models.Book;
using MyBook2.Services.Books;
using MyBook2.Services.Librarians;

namespace MyBook2.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookService books;
        private readonly ILibrarianService librarians;
        private readonly IMapper mapper;

        public BooksController(IBookService books, ILibrarianService librarians, IMapper mapper)
        {
            this.books = books;
            this.librarians = librarians;
            this.mapper = mapper;
        }

        //AllBooksQueryModel instead of (string author, string searchTerm, AllBooksSorting sorting) in the Method below!
        public IActionResult All([FromQuery]AllBooksQueryModel query)
        {
            var queryResult = this.books
                .All(query.Author, query.SearchTerm, query.CurrentPage, AllBooksQueryModel.BooksPerPage);

            var bookAuthors = this.books.AllAuthors();

            query.TotalBooks = queryResult.TotalBooks;
            query.Authors = bookAuthors;
            query.Books = queryResult.Books;

            return View(query);
        }

        [Authorize]
        public IActionResult Mine()
        {
            var myBooks = this.books.ByUser(User.Id());

            return View(myBooks);
        }

        [Authorize]
        public IActionResult Add()
        {
            if (!librarians.IsLibrarian(User.Id()))
            {
                return RedirectToAction(nameof(LibrariansController.Become), "Librarians");
            }

            return View(new BookFormModel
            {
                Genres = books.AllGenres()
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(BookFormModel book)
        {
            var librarianId = librarians.IdByUser(this.User.Id());

            if (librarianId == 0)
            {
                return RedirectToAction(nameof(LibrariansController.Become), "Librarians");
            }

            if (!books.GenreExists(book.GenreId))
            {
                ModelState.AddModelError(nameof(book.GenreId), "Genre does not exist.");
            }

            if (!ModelState.IsValid)
            {
                book.Genres = books.AllGenres();

                return View(book);
            }

            this.books.Create(book.Title, book.Author, book.Description, book.ImageUrl, book.GenreId, book.IssueYear, librarianId);

            return RedirectToAction(nameof(All));
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var userId = this.User.Id();

            if (!librarians.IsLibrarian(userId) && !User.UserIsAdmin())
            {
                return RedirectToAction(nameof(LibrariansController.Become), "Librarians");
            }

            var book = this.books.Details(id);

            if (book.UserId != userId && !User.UserIsAdmin())
            {
                return Unauthorized();
            }

            // AutoMapper when Linq (.Select query) has not been used - THERE'S A DIFFERENCE BETWEEN THESE TWO IMPLEMENTATIONS!
            var bookForm = mapper.Map<BookFormModel>(book);    

            bookForm.Genres = books.AllGenres();

            return View(bookForm);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(int id, BookFormModel book)
        {
            var librarianId = librarians.IdByUser(this.User.Id());

            if (librarianId == 0 && !User.UserIsAdmin())
            {
                return RedirectToAction(nameof(LibrariansController.Become), "Librarians");
            }

            if (!books.GenreExists(book.GenreId))
            {
                ModelState.AddModelError(nameof(book.GenreId), "Genre does not exist.");
            }

            if (!ModelState.IsValid)
            {
                book.Genres = books.AllGenres();

                return View(book);
            }

            if (!books.IsByLibrarian(id, librarianId) && !User.UserIsAdmin())
            {
                return BadRequest();
            }

            books.Edit(id, book.Title, book.Author, book.Description, book.ImageUrl, book.GenreId, book.IssueYear);

            return RedirectToAction(nameof(All));
        }
    }
}
