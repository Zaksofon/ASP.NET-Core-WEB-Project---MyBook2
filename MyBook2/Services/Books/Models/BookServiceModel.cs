namespace MyBook2.Services.Books.Models
{
    public class BookServiceModel : IBookModel
    {
        public int Id { get; init; }

        public string Title { get; init; }

        public string Author { get; init; }

        public string ImageUrl { get; init; }

        public string FilePDF { get; init; }

        public int IssueYear { get; init; }

        public string GenreName { get; init; }

        public bool IsPublic { get; init; }
    }
}
