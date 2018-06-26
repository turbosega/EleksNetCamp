using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Models.Entities;

namespace WebApi.Data.EntityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> user)
        {
            user.HasKey(u => u.Id);

            user.HasIndex(u => u.Login)
                .IsUnique();

            user.Property(u => u.Login)
                .HasMaxLength(20)
                .IsRequired();

            user.HasMany(u => u.Scores)
                .WithOne(score => score.User);

            user.Property(u => u.PasswordHash)
                .IsRequired();
        }
    }
}