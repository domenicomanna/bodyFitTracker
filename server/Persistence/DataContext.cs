using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<AppUser> AppUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppUser>().HasData(
                new AppUser{
                    Id = 1,
                    Email = "abc"
                },
                new AppUser {
                    Id = 2,
                    Email = "abcc"
                },
                new AppUser {
                    Id = 3,
                    Email = "abccdd"
                }
            );
        }
    }
}