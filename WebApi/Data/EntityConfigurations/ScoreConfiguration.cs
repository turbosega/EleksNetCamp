using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Models.Entities;

namespace WebApi.Data.EntityConfigurations
{
    public class ScoreConfiguration : IEntityTypeConfiguration<Score>
    {
        public void Configure(EntityTypeBuilder<Score> score)
        {
            score.HasKey(s => s.Id);

            score.HasIndex(s => new
            {
                s.UserId,
                s.GameId
            });

            score.HasOne(s => s.User)
                 .WithMany(user => user.Scores)
                 .HasForeignKey(s => s.UserId)
                 .OnDelete(DeleteBehavior.Cascade);

            score.HasOne(s => s.Game)
                 .WithMany(game => game.Scores)
                 .HasForeignKey(s => s.GameId);

            score.Property(s => s.Result)
                 .IsRequired();
        }
    }
}