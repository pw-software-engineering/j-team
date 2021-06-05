using HotelReservationSystem.Domain.Entities;
using HotelReservationSystem.Domain.ValueObjects;
using HotelReservationSystem.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservationSystem.Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var administratorRole = new IdentityRole("Administrator");

            if (roleManager.Roles.All(r => r.Name != administratorRole.Name))
            {
                await roleManager.CreateAsync(administratorRole);
            }

            var administrator = new ApplicationUser { UserName = "administrator@localhost", Email = "administrator@localhost" };

            if (userManager.Users.All(u => u.UserName != administrator.UserName))
            {
                await userManager.CreateAsync(administrator, "Administrator1!");
                await userManager.AddToRolesAsync(administrator, new[] { administratorRole.Name });
            }
        }

        public static async Task SeedSampleDataAsync(ApplicationDbContext context)
        {
            // Seed, if necessary
            if (!context.Hotels.Any())
            {
                context.Hotels.Add(new Hotel
                {
                    Name = "Hotel1",
                    Description = "Description of the Hotel1",
                    City = "City of the Hotel1",
                    Country = "Country of the Hotel1",
                    Password = BCrypt.Net.BCrypt.HashPassword("Hotel1"),
                    AccessToken = "99999999"
                });

                await context.SaveChangesAsync();
            }
            if (!context.Clients.Any())
            {
                var client = new Client
                {
                    Name = "client",
                    Surname = "client",
                    Email = "email@email.com",
                    Username = "client",
                    Password = BCrypt.Net.BCrypt.HashPassword("client"),
                };
                context.Clients.Add(client);
                client.AccessToken = client.ClientId.ToString();

                await context.SaveChangesAsync();
            }
        }
    }
}
