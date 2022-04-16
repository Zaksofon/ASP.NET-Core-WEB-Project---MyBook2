
using MyBook2.Controllers;
using MyBook2.Models.Librarians;
using MyTested.AspNetCore.Mvc;
using Xunit;

namespace MyBook2.Test.Routing
{
    public class LibrarianControllerTest
    {
        [Fact]
        public void GetBecomeShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Librarians/Become")
                .To<LibrariansController>(b => b.Become());

        [Fact]
        public void PostBecomeShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/Librarians/Become"))
                .To<LibrariansController>(b => b.Become(With.Any<BecomeLibrarianFormModel>()));
    }
}
