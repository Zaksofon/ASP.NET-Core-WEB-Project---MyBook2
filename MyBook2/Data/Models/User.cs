using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace MyBook2.Data.Models
{
    public class User : IdentityUser
    {
        [StringLength(30)]
        public string FullName { get; set; }
    }
}
