using MyBook2.Services.Books.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;

namespace MyBook2.Models.Book
{
    public class BookFormModel : IBookModel
    {
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Title must be text with length between 2 and 50 characters.")]
        public string Title { get; init; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Author must be text with length between 2 and 50 characters.")]
        public string Author { get; init; }

        [Required]
        [StringLength(1000, MinimumLength = 5, ErrorMessage = "Description must be text with length between 5 and 1000 characters.")]
        public string Description { get; init; }

        [Required]
        [Url]
        [Display(Name = "Image Url")]
        public string ImageUrl { get; init; }


        [Display(Name = "PDF File")]
        public string FilePDF { get; init; }

        [Range(800, 2022)]
        [Display(Name = "Issue Year")]
        public int IssueYear { get; init; }

        [Display(Name = "Genre")]
        public int GenreId { get; init; }

        public IEnumerable<BookGenreServiceModel> Genres { get; set; }
    }
}
