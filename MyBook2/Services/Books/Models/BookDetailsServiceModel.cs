using MyBook2.Services.Books.Models;

namespace MyBook2.Services.Books
{
    public class BookDetailsServiceModel : BookServiceModel
    {
        public string Description { get; init; }

        public int LibrarianId { get; init; }

        public string LibrarianName { get; init; }

        public int GenreId { get; init; }

        public string GenreName { get; init; }

        public string UserId { get; init; }
    }
}
