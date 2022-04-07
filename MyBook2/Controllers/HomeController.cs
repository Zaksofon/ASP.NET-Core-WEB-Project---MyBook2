using Microsoft.AspNetCore.Mvc;
using MyBook2.Data;
using MyBook2.Models;
using MyBook2.Models.Home;
using System.Diagnostics;
using System.Linq;
using MyBook2.Services.Statistics;

namespace MyBook2.Controllers
{
	public class HomeController : Controller
    {
        private readonly IStatisticsService statistics;
        private readonly MyBook2DbContext data;

        public HomeController(IStatisticsService statistics, MyBook2DbContext data)
        {
            this.statistics = statistics;
            this.data = data;
        }
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

            var totalStatistics = this.statistics.Total();

            return View(new IndexViewModel
            {
                TotalBooks = totalStatistics.TotalBooks,
                TotalUsers = totalStatistics.TotalUsers,
                Books = books,
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
