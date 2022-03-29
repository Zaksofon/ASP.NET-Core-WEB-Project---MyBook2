using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MyBook2.Data;
using MyBook2.Data.Models;
using MyBook2.Models.Book;

namespace MyBook2.Controllers
{
    public class BooksController : Controller
    {
        private readonly MyBook2DbContext data;

        public BooksController(MyBook2DbContext data) => this.data = data;

        public IActionResult Add() => View(new AddBookFormModel
        {
            Genres = GetBookGenre()
        });

        [HttpPost]
        public IActionResult Add(AddBookFormModel book)
        {
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
            };

            data.Books.Add(bookLibrary);

            data.SaveChanges(); 

            return RedirectToAction("Index", "Home");
        }

        private IEnumerable<BookGenreViewModel> GetBookGenre() => data
                .Genres
                .Select(g => new BookGenreViewModel()
                {
                    Id = g.Id,
                    Name = g.Name
                })
                .ToList();
    }
}
