
using System.Linq;
using FluentAssertions;
using MyBook2.Controllers;
using MyBook2.Data.Models;
using MyBook2.Models.Book;
using MyBook2.Models.Librarians;
using MyTested.AspNetCore.Mvc;
using Xunit;

namespace MyBook2.Test.Controllers
{
    using static WebConstants;
    public class LibrariansControllerTest
    {
        [Fact]
        public void BecomeShouldBeForAuthorizedUsersAndReturnView()
            => MyController<LibrariansController>
                .Instance()
                .Calling(b => b.Become())
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests())

                .AndAlso()
                .ShouldReturn()
                .View();

        [Theory]
        [InlineData("Librarian", "+359888888888")]
        public void PostBecomeShouldBeForAuthorizedUsersAndReturnRedirectWithValidModel(
            string librarianName,
            string phoneNumber)
            => MyController<LibrariansController>
                .Instance(controller => controller
                    .WithUser())
                .Calling(b => b.Become(new BecomeLibrarianFormModel
                {
                    Name = librarianName,
                    PhoneNumber = phoneNumber
                }))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests())

                .AndAlso()
                .ShouldHave()
                .ValidModelState()
                .Data(data => data
                    .WithSet<Librarian>(librarians => librarians
                        .Any(l =>
                            l.Name == librarianName &&
                            l.PhoneNumber == phoneNumber &&
                            l.UserId == TestUser.Identifier)))

                .AndAlso()
                .ShouldHave()
                .TempData(tempData => tempData
                    .ContainingEntryWithKey(GlobalMessageKey))

                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<BooksController>(b => b.All(With.Any<AllBooksQueryModel>())));
    }
}
