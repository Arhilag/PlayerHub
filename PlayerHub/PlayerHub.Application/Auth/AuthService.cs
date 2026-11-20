using Microsoft.EntityFrameworkCore;
using PlayerHub.Application.Abstractions;
using PlayerHub.Domain.Entities;

namespace PlayerHub.Application.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IAppDbContext _db;
        private readonly TimeProvider _timeProvider;

        public AuthService(IAppDbContext db, TimeProvider timeProvider)
        {
            _db = db;
            _timeProvider = timeProvider;
        }

        public async Task<GuestLoginResponse> LoginGuestAsync(GuestLoginRequest request, CancellationToken ct)
        {
            var now = _timeProvider.GetUtcNow();

            var player = await FindByDeviceIdAsync(request.DeviceId, ct);

            if (player is not null)
            {
                if (player.IsBanned)
                {
                    throw new PlayerBannedException();
                }

                player.Touch(now);
                try
                {
                    await _db.SaveChangesAsync(ct);
                }
                catch (DbUpdateConcurrencyException)
                {
                    
                }

                return ToResponse(player, false);
            }

            var nickname = string.IsNullOrWhiteSpace(request.Nickname)
                ? $"Player{Random.Shared.Next(1000, 10000)}"
                : request.Nickname.Trim();

            var newPlayer = new Player(Guid.CreateVersion7(), request.DeviceId, nickname, now);
            _db.Players.Add(newPlayer);

            try
            {
                await _db.SaveChangesAsync(ct);
                return ToResponse(newPlayer, true);
            }
            catch (DbUpdateException)
            {
                var existing = await FindByDeviceIdAsync(request.DeviceId, ct);
                if (existing is null)
                {
                    throw;
                }

                if (existing.IsBanned)
                {
                    throw new PlayerBannedException();
                }

                return ToResponse(existing, false);
            }
        }

        private Task<Player?> FindByDeviceIdAsync(string deviceId, CancellationToken ct)
        {
            return _db.Players.FirstOrDefaultAsync(p => p.DeviceId == deviceId, ct);
        }

        private static GuestLoginResponse ToResponse(Player player, bool isNew)
        {
            return new GuestLoginResponse
            {
                PlayerId = player.Id,
                Nickname = player.Nickname,
                Level = player.Level,
                Coins = player.Coins,
                IsNewPlayer = isNew
            };
        }
    }
}