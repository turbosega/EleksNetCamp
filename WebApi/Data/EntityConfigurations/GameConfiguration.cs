using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Models.Entities;

namespace WebApi.Data.EntityConfigurations
{
    public class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> game)
        {
            game.HasKey(g => g.Id);

            game.HasIndex(g => g.Title)
                .IsUnique();

            game.HasMany(g => g.Scores)
                .WithOne(score => score.Game);

            game.Property(g => g.Title)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}