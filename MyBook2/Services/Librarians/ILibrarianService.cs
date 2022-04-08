namespace MyBook2.Services.Librarians
{
    public interface ILibrarianService
    {
        public bool IsLibrarian(string userId);

        public int IdByUser(string userId);
    }
}
