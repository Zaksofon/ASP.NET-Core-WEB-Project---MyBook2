using Microsoft.AspNetCore.Mvc;
using MyBook2.Models;
using MyBook2.Models.Home;
using MyBook2.Services.Books;
using MyBook2.Services.Statistics;
using System.Diagnostics;
using System.Linq;

namespace MyBook2.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStatisticsService statistics;
        private readonly IBookService books;

        public HomeController(IStatisticsService statistics, IBookService books)
        {
            this.statistics = statistics;
            this.books = books;
        }
        public IActionResult Index()
        {
            //var totalBooks = this.data.Books.Count();

            var latestBooks = this.books.Latest().ToList(); 

            var totalStatistics = this.statistics.Total();

            return View(new IndexViewModel
            {
                TotalBooks = totalStatistics.TotalBooks,
                TotalUsers = totalStatistics.TotalUsers,
                Books = latestBooks
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
