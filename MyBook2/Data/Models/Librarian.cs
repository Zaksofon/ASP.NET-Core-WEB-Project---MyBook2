using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyBook2.Data.Models
{
    public class Librarian
    {
        public int Id { get; init; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [Required]
        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [Required]
        public string UserId { get; set; }

        public IEnumerable<Book> Books { get; init; } = new List<Book>(); 
    }
}
