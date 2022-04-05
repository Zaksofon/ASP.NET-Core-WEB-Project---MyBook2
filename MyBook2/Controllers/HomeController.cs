using Microsoft.AspNetCore.Mvc;
using MyBook2.Data;
using MyBook2.Models;
using MyBook2.Models.Home;
using System.Diagnostics;
using System.Linq;

namespace MyBook2.Controllers
{
	public class HomeController : Controller
    {
        private readonly MyBook2DbContext data;

        public HomeController(MyBook2DbContext data) => this.data = data;
        public IActionResult Index()
        {
            var totalBooks = this.data.Books.Count();

            var books = data
                .Books
                .OrderByDescending(b => b.Id)
                .Select(b => new BookIndexViewModel()
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    IssueYear = b.IssueYear,
                    ImageUrl = b.ImageUrl
                })
                .Take(3)
                .ToList();

            return View(new IndexViewModel
            {
                TotalBooks = totalBooks,
                Books = books,
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
