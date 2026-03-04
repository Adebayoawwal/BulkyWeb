using BulkyWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyWeb.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options
        ) : base(options)
        {
        }

        public DbSet<Catergory> Categories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Catergory>().HasData(
                new Catergory { Id = 1, Name = "Action", DiplayOrder = 1 },
                new Catergory { Id = 2, Name = "SciFi", DiplayOrder = 2 },
                new Catergory { Id = 3, Name = "History", DiplayOrder = 3 }
                );
       }
    }
}