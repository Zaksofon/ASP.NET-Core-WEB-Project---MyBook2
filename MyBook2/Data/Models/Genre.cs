using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyBook2.Data.Models
{
    public class Genre
    {
        public int Id { get; init; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public IEnumerable<Book> Books { get; init; } = new List<Book>();
    }
}
