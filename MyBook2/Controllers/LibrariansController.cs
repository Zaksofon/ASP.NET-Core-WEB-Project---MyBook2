using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBook2.Data;
using MyBook2.Data.Models;
using MyBook2.Infrastructure;
using MyBook2.Models.Librarians;

namespace MyBook2.Controllers
{
    public class LibrariansController : Controller
    {
        private readonly MyBook2DbContext data;

        public LibrariansController(MyBook2DbContext data) => this.data = data;

        [Authorize]
        public IActionResult Become() => View();

        [HttpPost]
        [Authorize]
        public IActionResult Become(BecomeLibrarianFormModel librarian)
        {
            var userId = User.GetId();

            var userIsAlreadyLibrarian = data
                .Librarians
                .Any(l => l.UserId == this.User.GetId());

            if (userIsAlreadyLibrarian)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(librarian);
            }

            var librarianData = new Librarian()
            {
                Name = librarian.Name,
                PhoneNumber = librarian.PhoneNumber,
                UserId = userId
            };

            data.Librarians.Add(librarianData);
            data.SaveChanges();

            return RedirectToAction("All", "Books");
        }
    }
}
