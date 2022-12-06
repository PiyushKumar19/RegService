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
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //    modelBuilder.HasSequence<int>("Id", schema: "dbo")
        //        .StartsAt(1)
        //    .IncrementsBy(1);

        //    modelBuilder.Entity<UsersRegModel>()
        //        .Property(o => o.Id)
        //        .HasDefaultValueSql("NEXT VALUE FOR dbo.Id");
        //}
        public DbSet<UsersRegistered> UsersRegistered { get; set; }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //    modelBuilder.HasSequence<int>("Id", schema: "dbo")
        //        .StartsAt(1)
        //    .IncrementsBy(1);

        //    modelBuilder.Entity<UsersRegModel>()
        //        .Property(o => o.Id)
        //        .HasDefaultValueSql("NEXT VALUE FOR dbo.Id");
        //}
        public DbSet<RegService.ViewModel.AccountDetailsViewModel> AccountDetailsViewModel { get; set; }
    }
}
