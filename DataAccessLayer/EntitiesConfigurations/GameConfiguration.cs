using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Entities;

namespace DataAccessLayer.EntitiesConfigurations
{
    public class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> game)
        {
            game.HasKey(g => g.Id);

            game.HasIndex(g => g.Title)
                .IsUnique();

            game.Property(g => g.Title)
                .HasMaxLength(50)
                .IsRequired();

            game.Property(g => g.About)
                .IsRequired();

            game.Property(g => g.ImageSrc)
                .IsRequired();

            game.HasMany(g => g.Results)
                .WithOne(result => result.Game)
                .HasForeignKey(result => result.GameId)
                .OnDelete(DeleteBehavior.Cascade);

            game.HasData(new Game
            {
                Id = 1,
                Title    = "Flappy Doggo",
                About    = "Flappy Bird on minimum",
                ImageSrc = "https://res.cloudinary.com/stnsfld/image/upload/v1533659984/wallhaven-32786.jpg"
            });
        }
    }
}