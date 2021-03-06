using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MyBook2.Services.Books.Models;

namespace MyBook2.Data.Models
{
    public class Book
    {
        public int Id { get; init; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [Required]
        [StringLength(30)]
        public string Author { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        public int GenreId { get; set; }
        public Genre Genre { get; init; }

        [Required]
        public string ImageUrl { get; set; }

        public string FilePDF { get; set; }

        public int IssueYear { get; set; }

        public int LibrarianId { get; init; }

        public Librarian Librarian { get; init; }

        public bool IsPublic { get; set; }
    }
}
