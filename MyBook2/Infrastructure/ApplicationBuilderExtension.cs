using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyBook2.Areas.Admin;
using MyBook2.Data;
using MyBook2.Data.Models;

namespace MyBook2.Infrastructure
{
    using static AdminConstants;
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(
            this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var services = serviceScope.ServiceProvider;

            MigrateDatabase(services);

            SeedGenres(services);
            SeedAdministrator(services);

            return app;
        }

        private static void MigrateDatabase(IServiceProvider services)
        {
            var data = services.GetRequiredService<MyBook2DbContext>();

            data.Database.Migrate();
        }

        private static void SeedGenres(IServiceProvider services)
        {
            var data = services.GetRequiredService<MyBook2DbContext>();

            if (data.Genres.Any())
            {
                return;
            }

            data.Genres.AddRange(new[]
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

        private static void SeedAdministrator(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            Task
                .Run(async () =>
                {
                    if (await roleManager.RoleExistsAsync(AdminRoleName))
                    {
                        return;
                    }

                    var role = new IdentityRole { Name = AdminRoleName };

                    await roleManager.CreateAsync(role);

                    const string adminEmail = "administrator@mybook.com";
                    const string adminPassword = "admin123";

                    var user = new User
                    {
                        Email = adminEmail,
                        UserName = adminEmail,
                        FullName = "Admin"
                    };

                    await userManager.CreateAsync(user, adminPassword);

                    await userManager.AddToRoleAsync(user, role.Name);
                })
                .GetAwaiter()
                .GetResult();
        }
    }
}
