using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyBook2.Data.Models;

namespace MyBook2.Data
{
    public class MyBook2DbContext : IdentityDbContext<User>
    {
        public MyBook2DbContext(DbContextOptions<MyBook2DbContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; init; }
        public DbSet<Genre> Genres { get; init; }
        public DbSet<Librarian> Librarians { get; init; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Book>()
                .HasOne(g => g.Genre)
                .WithMany(b => b.Books)
                .HasForeignKey(b => b.GenreId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Book>()
                .HasOne(l => l.Librarian)
                .WithMany(b => b.Books)
                .HasForeignKey(l => l.LibrarianId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Librarian>()
                .HasOne<User>()
                .WithOne()
                .HasForeignKey<Librarian>(l => l.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
