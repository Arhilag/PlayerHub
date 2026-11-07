using Microsoft.EntityFrameworkCore;
using PlayerHub.Domain.Entities;

namespace PlayerHub.Application.Abstractions
{
    public interface IAppDbContext
    {
        DbSet<Player> Players { get; }
        DbSet<AnalyticsEvent> AnalyticsEvents { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}