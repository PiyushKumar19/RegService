using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RegService.Models;
using RegService.ViewModel;
using System.Security.Principal;

namespace RegService.AppDbContext
{
    public class DatabaseContext : IdentityDbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<UsersRegModel> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasSequence<int>("FormNo", schema: "dbo")
                .StartsAt(10000000)
            .IncrementsBy(3035);

            modelBuilder.Entity<UsersRegModel>()
                .Property(o => o.Id)
                .HasDefaultValueSql("NEXT VALUE FOR dbo.FormNo");
        }
        public DbSet<RegService.ViewModel.RegisterViewModel> RegisterViewModel { get; set; }
        public DbSet<LoginTestingViewModel> LoginTestingViewModel { get; set; }
        public DbSet<UserRegStatus> UserRegStatuses { get; set; }
    }
}
