using Microsoft.EntityFrameworkCore;
using MSQBot_API.Entities.Models;

namespace MSQBot_API.Repository
{
    public class MSQBotDbContext : DbContext
    {
        public MSQBotDbContext(DbContextOptions<MSQBotDbContext> options) : base(options)
        {
        }

        public DbSet<Movie> movies { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Rate> rates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rate>()
                .HasKey(r => new { r.MovieId, r.UserId });
        }
    }
}