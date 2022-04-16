
using System.Collections.Generic;
using MyBook2.Controllers;
using MyBook2.Models.Home;
using MyBook2.Services.Books.Models;
using MyTested.AspNetCore.Mvc;
using Xunit;

namespace MyBook2.Test.PipeLine
{
    using static Data.Books;
    public class HomeControllerTest
    {
        [Fact]
        public void IndexShouldReturnViewWithCorrectModelAndData()
            => MyMvc
                .Pipeline()
                .ShouldMap("/")
                .To<HomeController>(b => b.Index())
                .Which(controller => controller
                    .WithData(TenPublicBooks))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<IndexViewModel>());

        [Fact]
        public void ErrorShouldReturnView()
            => MyMvc
                .Pipeline()
                .ShouldMap("/Home/Error")
                .To<HomeController>(b => b.Error())
                .Which()
                .ShouldReturn()
                .View();
    }
}
