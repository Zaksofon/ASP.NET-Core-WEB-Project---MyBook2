using MyBook2.Data;
using System.Collections.Generic;
using System.Linq;

namespace MyBook2.Services.Books
{
    public class BookService : IBookService
    {
        private readonly MyBook2DbContext data;

        public BookService(MyBook2DbContext data)
        {
            this.data = data;
        }
        public BookQueryServiceModel All(string author, string searchTerm, int currentPage, int booksPerPage)
        {
            var booksQuery = data.Books.AsQueryable();

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

            var books = booksQuery
                .Skip((currentPage - 1) * booksPerPage)
                .Take(booksPerPage)
                .Select(b => new BookServiceModel
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    IssueYear = b.IssueYear,
                    ImageUrl = b.ImageUrl,
                    Genre = b.Genre.Name
                })
                .ToList();

            return new BookQueryServiceModel
            {
                TotalBooks = totalBooks,
                CurrentPage = currentPage,
                BooksPerPage = booksPerPage,
                Books = books
            };
        }

        public IEnumerable<string> AllBooksAuthors()
        {
            return data
                .Books
                .Select(b => b.Author)
                .Distinct()
                .OrderBy(au => au)
                .ToList();
        }
    }
}
