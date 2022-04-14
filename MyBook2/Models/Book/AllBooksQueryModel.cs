using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MyBook2.Services.Books;
using MyBook2.Services.Books.Models;

namespace MyBook2.Models.Book
{
    public class AllBooksQueryModel
    {
        public const int BooksPerPage = 3; 

        [Display (Name = "Search by text")]
        public string SearchTerm { get; init; }

        //public AllBooksSorting Sorting { get; init; }

        public int CurrentPage { get; init; } = 1;

        public int TotalBooks { get; set; } 

        public string Author { get; init; }

        public IEnumerable<string> Authors { get; set; }

        public IEnumerable<BookServiceModel> Books { get; set; }
    }
}
