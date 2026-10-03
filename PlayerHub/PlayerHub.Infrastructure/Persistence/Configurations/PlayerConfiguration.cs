using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlayerHub.Domain.Entities;

namespace PlayerHub.Infrastructure.Persistence.Configurations
{
    internal class PlayerConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.ToTable("players");

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedNever();

            builder.Property(p => p.DeviceId)
                .IsRequired()
                .HasMaxLength(Player.DeviceIdMaxLength);

            builder.HasIndex(p => p.DeviceId).IsUnique();

            builder.Property(p => p.Nickname)
                .IsRequired()
                .HasMaxLength(Player.NicknameMaxLength);

            builder.Property(p => p.CreatedAt).IsRequired();
            builder.Property(p => p.LastSeenAt).IsRequired();

            builder.Property(p => p.Version)
                .IsRowVersion();

            builder.HasIndex(p => p.Nickname);
            builder.HasIndex(p => p.LastSeenAt);
            builder.HasIndex(p => p.CreatedAt);
        }
    }
}
