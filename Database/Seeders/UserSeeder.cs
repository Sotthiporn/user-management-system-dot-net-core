using user_management_dot_net_core.Models;

namespace user_management_dot_net_core.Database.Seeders
{
    public class UserSeeder
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            if (!context.Users.Any())
            {
                var admin = new User
                {
                    Username = "admin",
                    Email = "admin@mptc.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
                    Role = "Admin",
                    RefreshToken = null,
                    RefreshTokenExpiry = null
                };
                context.Users.Add(admin);
                await context.SaveChangesAsync();
            }
        }
    }
}
