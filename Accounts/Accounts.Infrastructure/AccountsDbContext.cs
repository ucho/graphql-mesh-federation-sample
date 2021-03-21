using Microsoft.EntityFrameworkCore;
using System;
using Accounts.Domain;

namespace Accounts.Infrastructure
{
    public class AccountsDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public AccountsDbContext(DbContextOptions<AccountsDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Email = "user01@example.com", Password = "user01" },
                new User { Id = 2, Email = "user02@example.com", Password = "user02" }
            );
        }
    }
}