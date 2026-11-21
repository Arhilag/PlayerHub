using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlayerHub.Domain.Entities;

namespace PlayerHub.Infrastructure.Persistence.Configurations
{
    internal class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable("refresh_tokens");

            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).ValueGeneratedNever();

            builder.Property(t => t.TokenHash)
                .IsRequired()
                .HasMaxLength(RefreshToken.TokenHashLength);

            builder.HasIndex(t => t.TokenHash).IsUnique();

            builder.Property(t => t.CreatedAt).IsRequired();
            builder.Property(t => t.ExpiresAt).IsRequired();

            builder.HasOne<Player>().WithMany()
                .HasForeignKey(t => t.PlayerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
