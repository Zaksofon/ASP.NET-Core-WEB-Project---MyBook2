using System;
using System.Collections.Generic;
using MyBook2.Controllers;
using MyBook2.Models.Home;
using MyBook2.Services.Books.Models;
using MyTested.AspNetCore.Mvc;
using Xunit;

using static MyBook2.Test.Data.Books;

namespace MyBook2.Test.Controllers
{
    using static WebConstants.Cache;

    public class HomeControllerTest
    {
        [Fact]
        public void IndexShouldReturnCorrectViewWithModel()
            => MyController<HomeController>
                .Instance(controller => controller
                    .WithData(TenPublicBooks))
                .Calling(b => b.Index())
                .ShouldHave()
                .MemoryCache(cache => cache
                    .ContainingEntry(entry => entry
                        .WithKey(LatestBooksCacheKey)
                        .WithAbsoluteExpirationRelativeToNow(TimeSpan.FromMinutes(15))
                        .WithValueOfType<List<LatestBookServiceModel>>()))

                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<IndexViewModel>());

        [Fact]
        public void ErrorShouldReturnView()
            => MyController<HomeController>
                .Instance()
                .Calling(b => b.Error())
                .ShouldReturn()
                .View();
    }
}