using System.ComponentModel.DataAnnotations;

namespace MyBook2.Models.Librarians
{
    public class BecomeLibrarianFormModel
    {
        public int Id { get; init; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 6)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

    }
}
