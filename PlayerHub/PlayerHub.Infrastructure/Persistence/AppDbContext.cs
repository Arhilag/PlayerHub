using Microsoft.EntityFrameworkCore;
using PlayerHub.Domain.Entities;

namespace PlayerHub.Infrastructure.Persistence
{
    internal class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Player> Players => Set<Player>();
        public DbSet<AnalyticsEvent> AnalyticsEvents => Set<AnalyticsEvent>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
