using Backend.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.BLL.Data;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.Property(p => p.Id).HasColumnName("userId");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name).HasColumnName("name");
        builder.Property(p => p.Email).HasColumnName("email");
        builder.Property(p => p.PasswordSalt).HasColumnName("passwordSalt");
        builder.Property(p => p.Password).HasColumnName("password");
        builder.Property(p => p.LastName).HasColumnName("lastName");
        builder.Property(p => p.Capital).HasColumnName("capital");
        builder.Property(p => p.Status).HasColumnName("status");

    }
}