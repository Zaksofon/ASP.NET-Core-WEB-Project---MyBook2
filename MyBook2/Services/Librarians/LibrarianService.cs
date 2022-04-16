using MyBook2.Data;
using System.Linq;

namespace MyBook2.Services.Librarians
{
    public class LibrarianService : ILibrarianService
    {
        private readonly MyBook2DbContext data;

        public LibrarianService(MyBook2DbContext data)
        {
            this.data = data;
        }

        public bool IsLibrarian(string userId) => data
                .Librarians
                .Any(l => l.UserId == userId);

        public int IdByUser(string userId)
        {
            return data
                .Librarians
                .Where(l => l.UserId == userId)
                .Select(l => l.Id)
                .FirstOrDefault();
        }
    }
}
