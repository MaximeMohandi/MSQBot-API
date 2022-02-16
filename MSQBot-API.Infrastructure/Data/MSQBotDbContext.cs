using MSQBot_API.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace MSQBot_API.Infrastructure
{
    public class MSQBotDbContext : DbContext
    {
        public MSQBotDbContext(DbContextOptions<MSQBotDbContext> options) : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Rate> Rates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Rate>().HasKey(r => new { r.MovieId, r.UserId });
        }
    }
}