
using System.Collections.Generic;
using System.Linq;
using MyBook2.Data.Models;

namespace MyBook2.Test.Data
{
    public static class Books
    {
        public static IEnumerable<Book> TenPublicBooks
            => Enumerable.Range(0, 10)
                .Select(i => new Book
                {
                    IsPublic = true
                });
    }
}
