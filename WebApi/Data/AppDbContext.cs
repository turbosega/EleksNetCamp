using Microsoft.EntityFrameworkCore;
using WebApi.Data.EntityConfigurations;
using WebApi.Models.Entities;

namespace WebApi.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User>  Users  { get; set; }
        public DbSet<Game>  Games  { get; set; }
        public DbSet<Score> Scores { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new GameConfiguration());
            modelBuilder.ApplyConfiguration(new ScoreConfiguration());
        }
    }
}