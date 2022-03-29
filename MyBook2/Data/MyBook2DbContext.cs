using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using MyBook2.Data.Models;

namespace MyBook2.Data
{
    public class MyBook2DbContext : IdentityDbContext
    {
        public MyBook2DbContext(DbContextOptions<MyBook2DbContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; init; }
        public DbSet<Genre> Genres { get; init; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Book>()
                .HasOne(g => g.Genre)
                .WithMany(b => b.Books)
                .HasForeignKey(b => b.GenreId)
                .OnDelete(DeleteBehavior.Restrict);
            base.OnModelCreating(builder);
        }
    }
}
