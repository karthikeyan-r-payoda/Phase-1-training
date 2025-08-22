using Microsoft.EntityFrameworkCore;
using MiniProject2.Models;

namespace MiniProject2.Context
{
    public class MyAppDbContext : DbContext
    {
        public MyAppDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<BankModel> BankDetails { get; set; }

        public DbSet<CustomerModel> customerDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // One-to-many relationship
            modelBuilder.Entity<CustomerModel>()
                .HasMany(c => c.BankAccounts)
                .WithOne(b => b.Customer)
                .HasForeignKey(b => b.CustId);
        }
    }
}
