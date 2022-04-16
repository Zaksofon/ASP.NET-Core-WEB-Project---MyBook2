using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBook2.Services.Books;

namespace MyBook2.Areas.Admin.Controllers
{
    using static AdminConstants;

    [Area(AreaName)]
    [Authorize(Roles = AdminRoleName)]
    public class BooksController : AdminController
    {
        private readonly IBookService books;

        public BooksController(IBookService books) => this.books = books;

        public IActionResult All()
        {
            var books = this.books
                .All(publicOnly: false)
                .Books;

            return View(books);
        }

        public IActionResult Change(int id)
        {
            books.Change(id);

            return RedirectToAction(nameof(All));
        }
        
        public IActionResult Delete(int id)
        {
            books.Delete(id);

            return RedirectToAction(nameof(All));
        }
    }
}
