using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MyBook2.Models.Home;
using MyBook2.Services.Books;
using MyBook2.Services.Books.Models;
using MyBook2.Services.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyBook2.Controllers
{
    using static WebConstants.Cache;
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

            var latestBooks = cache.Get<List<LatestBookServiceModel>>(LatestBooksCacheKey);

            if (latestBooks == null)
            {
                latestBooks = books
                    .Latest()
                    .ToList();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(15));

                cache.Set(LatestBooksCacheKey, latestBooks, cacheOptions);
            }

            var totalStatistics = this.statistics.Total();

            return View(new IndexViewModel
            {
                TotalBooks = totalStatistics.TotalBooks,
                TotalUsers = totalStatistics.TotalUsers,
                Books = latestBooks
            });
        }
        public IActionResult Error() => View();
    }
}
