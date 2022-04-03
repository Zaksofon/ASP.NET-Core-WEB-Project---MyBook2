using System;
using Microsoft.AspNetCore.Mvc;
using MyBook2.Data;
using MyBook2.Data.Models;
using MyBook2.Models.Book;
using System.Collections.Generic;
using System.Linq;

namespace MyBook2.Controllers
{
    public class BooksController : Controller
    {
        private readonly MyBook2DbContext data;

        public BooksController(MyBook2DbContext data) => this.data = data;

        //AllBooksQueryModel instead of (string author, string searchTerm, AllBooksSorting sorting) in the Method below!
        public IActionResult All([FromQuery]AllBooksQueryModel query)
        {
            var booksQuery = data.Books.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Author))
            {
                booksQuery = booksQuery
                    .Where(b => b.Author == query.Author);
            }

            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                booksQuery = booksQuery
                    .Where(b => (b.Title + " " + b.Author).ToLower().Contains(query.SearchTerm.ToLower())
                                || b.Description.ToLower().Contains(query.SearchTerm.ToLower()));
            }

            //booksQuery = query.Sorting switch
            //{
            //    AllBooksSorting.MostRecentlyAdded => booksQuery.OrderByDescending(b => b.Id),
            //    AllBooksSorting.IssueDate => booksQuery.OrderByDescending(b => b.IssueYear),
            //    AllBooksSorting.TitleAndAuthor => booksQuery.OrderByDescending(b => b.Title).ThenBy(b => b.Author),
            //    _ => booksQuery.OrderByDescending(b => b.Id)
            //};

            var totalBooks = booksQuery.Count();

            var books = booksQuery
                .Skip((query.CurrentPage - 1) * AllBooksQueryModel.BooksPerPage)
                .Take(AllBooksQueryModel.BooksPerPage)
                .Select(b => new BookListingViewModel()
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    IssueYear = b.IssueYear,
                    ImageUrl = b.ImageUrl,
                    Genre = b.Genre.Name
                })
                .ToList();

            var bookAuthors = data
                .Books
                .Select(b => b.Author)
                .Distinct()
                .OrderBy(au => au)
                .ToList();

            query.TotalBooks = totalBooks;
            query.Authors = bookAuthors;
            query.Books = books;

            return View(query);
        }
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
    }
}
