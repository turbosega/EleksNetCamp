using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Entities;
using Models.Enumerations;

namespace DataAccessLayer.EntitiesConfigurations
{
    public class ResultConfiguration : IEntityTypeConfiguration<Result>
    {
        public void Configure(EntityTypeBuilder<Result> result)
        {
            result.HasKey(r => r.Id);

            result.HasIndex(r => new
            {
                r.UserId,
                r.GameId
            });

            result.Property(r => r.GameOutcome)
                  .HasConversion(outcome => outcome.ToString(),
                                 outcome => (GameOutcome) Enum.Parse(typeof(GameOutcome), outcome))
                  .HasMaxLength(20);

            result.Property(r => r.DateTime)
                  .HasColumnType("datetime")
                  .ValueGeneratedOnAdd()
                  .IsRequired();

            result.HasOne(r => r.User)
                  .WithMany(user => user.Results)
                  .HasForeignKey(r => r.UserId)
                  .OnDelete(DeleteBehavior.Cascade);

            result.HasOne(r => r.Game)
                  .WithMany(game => game.Results)
                  .HasForeignKey(r => r.GameId)
                  .OnDelete(DeleteBehavior.Cascade);
        }
    }
}