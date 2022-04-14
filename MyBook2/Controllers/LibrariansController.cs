using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBook2.Data;
using MyBook2.Data.Models;
using MyBook2.Infrastructure.Extensions;
using MyBook2.Models.Librarians;
using System.Linq;

namespace MyBook2.Controllers
{
    using static WebConstants;
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
            var userId = User.Id();

            var userIsAlreadyLibrarian = data
                .Librarians
                .Any(l => l.UserId == this.User.Id());

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

            TempData[GlobalMessageKey] = "You are now a Librarian!";

            return RedirectToAction("All", "Books");
        }
    }
}
