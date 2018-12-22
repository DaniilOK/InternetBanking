using IB.Data.EF.Configurations;
using IB.Repository.Interface.Models;
using Microsoft.EntityFrameworkCore;

namespace IB.Data.EF
{
    public class InternetBankingContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Permission> Permissions { get; set; }

        public DbSet<UserPermission> UserPermissions { get; set; }

        public DbSet<BankAccount> BankAccounts { get; set; }

        public DbSet<BankCard> BankCards { get; set; }

        public InternetBankingContext(DbContextOptions<InternetBankingContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.AddConfiguration(new UserConfiguration());
            modelBuilder.AddConfiguration(new PermissionConfiguration());
            modelBuilder.AddConfiguration(new UserPermissionConfiguration());
            modelBuilder.AddConfiguration(new BankAccountConfiguration());
            modelBuilder.AddConfiguration(new BankCardConfiguration());
        }
    }
}
