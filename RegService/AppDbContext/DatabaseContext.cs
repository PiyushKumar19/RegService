using Microsoft.EntityFrameworkCore;
using RegService.Models;

namespace RegService.AppDbContext
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<UsersRegModel> Users { get; set; }
    }
}
