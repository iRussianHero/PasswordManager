using Microsoft.EntityFrameworkCore;

namespace PasswordManager.Models
{
    public class ManagerDbContext : DbContext
    {
        public ManagerDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Item> Items { get; set; }
        public DbSet<Type> Types { get; set; }
    }
}
