using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MyBook2.Models;
using MyBook2.Models.Home;
using MyBook2.Services.Books;
using MyBook2.Services.Statistics;
using System.Diagnostics;
using System.Linq;
using Microsoft.Extensions.Caching.Memory;

namespace MyBook2.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStatisticsService statistics;
        private readonly IBookService books;
        private readonly IMemoryCache cache;

        public HomeController(IStatisticsService statistics, IBookService books, IMemoryCache cache)
        {
            this.statistics = statistics;
            this.books = books;
            this.cache = cache;
        }
        public IActionResult Index()
        {
            //var totalBooks = this.data.Books.Count();

            const string latestBooksCacheKey = "LatestBooksCacheKey";

            var latestBooks = cache.Get<List<LatestBookServiceModel>>(latestBooksCacheKey);

            if (latestBooks == null)
            {
                latestBooks = books
                    .Latest()
                    .ToList();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(15));

                cache.Set(latestBooksCacheKey, latestBooks);
            }

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
