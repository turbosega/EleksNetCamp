using System;
using System.Collections.Generic;
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

            user.Property(u => u.PasswordHash)
                .IsRequired();

            user.Property(u => u.UserType)
                .IsRequired();

            user.HasMany(u => u.Results)
                .WithOne(result => result.User)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}