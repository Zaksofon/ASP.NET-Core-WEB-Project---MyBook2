namespace MyBook2.Services.Books.Models
{
    public interface IBookModel
    {
        string Title { get; }
        string Author { get; }
        int IssueYear { get; }
    }
}
