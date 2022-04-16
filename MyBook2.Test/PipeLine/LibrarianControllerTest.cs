
using System.Linq;
using MyBook2.Controllers;
using MyBook2.Data.Models;
using MyBook2.Models.Book;
using MyBook2.Models.Librarians;
using MyTested.AspNetCore.Mvc;
using Xunit;

namespace MyBook2.Test.PipeLine
{
    using static WebConstants;
    public class LibrarianControllerTest
    {
        [Fact]
        public void GetBecomeShouldBeForAuthorizedUsersAndReturnView()
           => MyPipeline
               .Configuration()
               .ShouldMap(request => request
                   .WithPath("/Librarians/Become")
                   .WithUser())
               .To<LibrariansController>(b => b.Become())
               .Which()
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
            => MyPipeline
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/Librarians/Become")
                    .WithMethod(HttpMethod.Post)
                    .WithFormFields(new
                    {
                        Name = librarianName,
                        PhoneNumber = phoneNumber
                    })
                    .WithUser()
                    .WithAntiForgeryToken())
                .To<LibrariansController>(b => b.Become(new BecomeLibrarianFormModel
                {
                    Name = librarianName,
                    PhoneNumber = phoneNumber
                }))
                .Which()
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests())
                .ValidModelState()
                .Data(data => data
                    .WithSet<Librarian>(librarian => librarian
                        .Any(l =>
                            l.Name == librarianName &&
                            l.PhoneNumber == phoneNumber &&
                            l.UserId == TestUser.Identifier)))
                .TempData(tempData => tempData
                    .ContainingEntryWithKey(GlobalMessageKey))
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<BooksController>(b => b.All(With.Any<AllBooksQueryModel>())));
    }
}
