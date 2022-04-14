using AutoMapper;
using AutoMapper.QueryableExtensions;
using MyBook2.Data;
using MyBook2.Data.Models;
using MyBook2.Models.Home;
using MyBook2.Services.Books.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyBook2.Services.Books
{
    public class BookService : IBookService
    {
        private readonly MyBook2DbContext data;
        private readonly IConfigurationProvider mapper;

        public BookService(MyBook2DbContext data, IConfigurationProvider mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public int Create(string title, string author, string description, string imageUrl, int genreId, int issueYear, int librarianId)
        {
            var bookLibrary = new Book
            {
                Title = title,
                Author = author,
                Description = description,
                ImageUrl = imageUrl,
                GenreId = genreId,
                IssueYear = issueYear,
                LibrarianId = librarianId,
                IsPublic = false
            };

            data.Books.Add(bookLibrary);
            data.SaveChanges();

            return bookLibrary.Id;
        }

        public bool Edit(int id, string title, string author, string description, string imageUrl, int genreId, int issueYear, bool isPublic)
        {
            var bookLibrary = this.data.Books.Find(id);

            if (bookLibrary == null)
            {
                return false;
            }

            bookLibrary.Title = title;
            bookLibrary.Author = author;
            bookLibrary.Description = description;
            bookLibrary.ImageUrl = imageUrl;
            bookLibrary.GenreId = genreId;
            bookLibrary.IssueYear = issueYear;
            bookLibrary.IsPublic = isPublic;

            data.SaveChanges();

            return true;
        }

        public BookQueryServiceModel All(
            string author = null,
            string searchTerm = null,
            int currentPage = 1,
            int booksPerPage = int.MaxValue,
            bool publicOnly = true)
        {
            var booksQuery = data.Books
                .Where(b => !publicOnly || b.IsPublic);

            if (!string.IsNullOrWhiteSpace(author))
            {
                booksQuery = booksQuery
                    .Where(b => b.Author == author);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                booksQuery = booksQuery
                    .Where(b => (b.Title + " " + b.Author).ToLower().Contains(searchTerm.ToLower())
                                || b.Description.ToLower().Contains(searchTerm.ToLower()));
            }

            //booksQuery = query.Sorting switch
            //{
            //    AllBooksSorting.MostRecentlyAdded => booksQuery.OrderByDescending(b => b.Id),
            //    AllBooksSorting.IssueDate => booksQuery.OrderByDescending(b => b.IssueYear),
            //    AllBooksSorting.TitleAndAuthor => booksQuery.OrderByDescending(b => b.Title).ThenBy(b => b.Author),
            //    _ => booksQuery.OrderByDescending(b => b.Id)
            //};

            var totalBooks = booksQuery.Count();

            var books = GetBooks(booksQuery
                .Skip((currentPage - 1) * booksPerPage)
                .Take(booksPerPage));

            return new BookQueryServiceModel
            {
                TotalBooks = totalBooks,
                CurrentPage = currentPage,
                BooksPerPage = booksPerPage,
                Books = books
            };
        }

        public IEnumerable<LatestBookServiceModel> Latest()
        {
            return data
                .Books
                .Where(b => b.IsPublic)
                .OrderByDescending(b => b.Id)
                .ProjectTo<LatestBookServiceModel>(this.mapper) // AutoMapper instead of Linq (.Select query)
                .Take(3)
                .ToList();
        }

        public BookDetailsServiceModel Details(int id)
        {
            return this.data
                .Books
                .Where(b => b.Id == id)
                .ProjectTo<BookDetailsServiceModel>(this.mapper) // AutoMapper instead of Linq (.Select query)
                .FirstOrDefault();
        }

        public IEnumerable<BookServiceModel> ByUser(string userId)
        {
            return GetBooks(data
                .Books
                .Where(b => b.Librarian.UserId == userId));
        }

        public bool IsByLibrarian(int bookId, int librarianId)
        {
            return data.Books.Any(b => b.Id == bookId && b.LibrarianId == librarianId);
        }

        public void Change(int bookId)
        {
            var book = data.Books.Find(bookId);

            book.IsPublic = !book.IsPublic;

            data.SaveChanges();
        }

        public IEnumerable<string> AllAuthors()
        {
            return data
                .Books
                .Select(b => b.Author)
                .Distinct()
                .OrderBy(au => au)
                .ToList();
        }

        public IEnumerable<BookGenreServiceModel> AllGenres()
        {
            return data
                .Genres
                .ProjectTo<BookGenreServiceModel>(mapper)
                .ToList();
        }

        public bool GenreExists(int genreId) =>
            data.Genres.Any(g => g.Id == genreId);

        private IEnumerable<BookServiceModel> GetBooks(IQueryable<Book> bookQuery)
        {
            return bookQuery
                .ProjectTo<BookServiceModel>(mapper)
                .ToList();
        }
    }
}
