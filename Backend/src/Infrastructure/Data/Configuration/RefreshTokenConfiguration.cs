using Domain.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable("RefreshTokens");

        builder.HasKey(refreshToken => refreshToken.Id);

        builder.Property(refreshToken => refreshToken.Token).HasMaxLength(200);

        builder.HasIndex(refreshToken => refreshToken.Token).IsUnique();

        builder.Property(refreshToken => refreshToken.UserId).IsRequired();

        builder.Property(refreshToken => refreshToken.ExpiresOnUtc).IsRequired();
    }
}