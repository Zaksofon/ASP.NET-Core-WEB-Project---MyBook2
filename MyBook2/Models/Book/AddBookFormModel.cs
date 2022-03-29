﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MyBook2.Data.Models;

namespace MyBook2.Models.Book
{
    public class AddBookFormModel
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

        [Range(800, 2022)]
        [Display(Name = "Issue Year")]
        public int IssueYear { get; init; }

        [Display(Name = "Genre")]
        public int GenreId { get; init; }

        public IEnumerable<BookGenreViewModel> Genres { get; set; }
    }
}
