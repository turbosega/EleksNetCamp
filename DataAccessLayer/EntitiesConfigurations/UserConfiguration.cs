using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Entities;
using Models.Enumerations;

namespace DataAccessLayer.EntitiesConfigurations
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

            user.Property(u => u.AvatarUrl)
                .IsRequired();

            user.Property(u => u.PasswordHash)
                .IsRequired();

            user.Property(u => u.UserType)
                .IsRequired();

            user.HasMany(u => u.Results)
                .WithOne(result => result.User)
                .OnDelete(DeleteBehavior.Cascade);

            user.HasData(new User
            {
                Id = 1,
                Login        = "Fry",
                AvatarUrl    = "https://res.cloudinary.com/stnsfld/image/upload/v1533657580/gghsflish4e1jr43q2yd.png",
                PasswordHash = "AQAAAAEAACcQAAAAEAyTA6a5i6/Ns6VkkhDgh345S9gj+aYUPTmJRBO2TJzQdMOxJApGpWn9k6XL5+VtfA==",
                UserType     = UserType.Administrator
            });
        }
    }
}