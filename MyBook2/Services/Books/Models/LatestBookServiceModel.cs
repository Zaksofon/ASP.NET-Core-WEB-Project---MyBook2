namespace MyBook2.Services.Books.Models
{
    public class LatestBookServiceModel : IBookModel
    {
        public int Id { get; init; }

        public string Title { get; init; }

        public string Author { get; init; }

        public string ImageUrl { get; init; }

        public int IssueYear { get; init; }
    }
}
