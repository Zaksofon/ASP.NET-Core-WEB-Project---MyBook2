using MyBook2.Services.Books.Models;

namespace MyBook2.Infrastructure.Extensions
{
    public static class ModelExtensions
    {
        public static string GetInformation(this IBookModel book)
            => book.Author + "-" + book.Title + "-" + book.IssueYear;
    }
}
