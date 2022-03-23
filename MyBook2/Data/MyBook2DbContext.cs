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
    }
}
