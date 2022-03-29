using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyBook2.Data;
using MyBook2.Data.Models;

namespace MyBook2.Infrastructure
{
    public static class ApplicationBuilderExtension
    {
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();

            var data = scopedServices.ServiceProvider.GetService<MyBook2DbContext>();

            data.Database.Migrate();

            SeedCategories(data);

            return app;
        }

        private static void SeedCategories(MyBook2DbContext data)
        {
            if (data.Genres.Any())
            {
               return;
            }

            data.Genres.AddRange(new []
            {
                new Genre {Name = "Humor"},
                new Genre {Name = "Sci-Fi"},
                new Genre {Name = "Mystery"},
                new Genre {Name = "Fantasy"},
                new Genre {Name = "Adventure"},
                new Genre {Name = "Thriller"},
                new Genre {Name = "Romance"},
                new Genre {Name = "Historical"},
                new Genre {Name = "Crime"},
                new Genre {Name = "Children's"},
                new Genre {Name = "Cooking"},
                new Genre {Name = "Art"},
                new Genre {Name = "Travel"}
            });

            data.SaveChanges();
        }
    }
}
