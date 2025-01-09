using Microsoft.EntityFrameworkCore;
using user_management_dot_net_core.Models;

namespace user_management_dot_net_core.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }

}
