using DataAccessLayer.EntitiesConfigurations;
using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace DataAccessLayer
{
    public class AppDbContext : DbContext
    {
        public virtual DbSet<User>   Users   { get; set; }
        public virtual DbSet<Game>   Games   { get; set; }
        public virtual DbSet<Result> Results { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new GameConfiguration());
            modelBuilder.ApplyConfiguration(new ResultConfiguration());
        }
    }
}