using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlayerHub.Domain.Entities;

namespace PlayerHub.Infrastructure.Persistence.Configurations
{
    internal class AnalyticsEventConfiguration : IEntityTypeConfiguration<AnalyticsEvent>
    {
        public void Configure(EntityTypeBuilder<AnalyticsEvent> builder)
        {
            builder.ToTable("analytics_events");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedNever();

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(AnalyticsEvent.NameMaxLength);

            builder.Property(e => e.Parameters)
                .IsRequired()
                .HasColumnType("jsonb");

            builder.HasOne<Player>()
                .WithMany()
                .HasForeignKey(e => e.PlayerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(e => e.SessionId)
                .IsRequired();

            builder.Property(e => e.ClientTime).IsRequired();
            builder.Property(e => e.ServerTime).IsRequired();

            builder.HasIndex(e => new { e.Name, e.ServerTime });
            builder.HasIndex(e => new { e.PlayerId, e.ServerTime });
            builder.HasIndex(e => e.SessionId);
        }
    }
}
