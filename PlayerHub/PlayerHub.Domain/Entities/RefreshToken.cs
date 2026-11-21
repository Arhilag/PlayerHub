namespace PlayerHub.Domain.Entities
{
    public class RefreshToken
    {
        public const int TokenHashLength = 64;

        public Guid Id { get; private set; }
        public Guid PlayerId { get; private set; }
        public string TokenHash { get; private set; } = null!;
        public DateTimeOffset CreatedAt { get; private set; }
        public DateTimeOffset ExpiresAt { get; private set; }
        public DateTimeOffset? RevokedAt { get; private set; }
        public Guid? ReplacedByTokenId { get; private set; }

        public bool IsRevoked => RevokedAt.HasValue;
        public bool IsExpired(DateTimeOffset now) => now >= ExpiresAt;
        public bool IsActive(DateTimeOffset now) => !IsRevoked && !IsExpired(now);

        private RefreshToken() { }

        public RefreshToken(Guid id, Guid playerId, string tokenHash, DateTimeOffset now, DateTimeOffset expiresAt)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Id must not be empty", nameof(id));
            }
            if (playerId == Guid.Empty)
            {
                throw new ArgumentException("PlayerId must not be empty", nameof(playerId));
            }
            ValidateTokenHash(tokenHash);
            if (expiresAt <= now)
            {
                throw new ArgumentException("ExpiresAt must be later than now", nameof(expiresAt));
            }

            Id = id;
            PlayerId = playerId;
            TokenHash = tokenHash;
            CreatedAt = now;
            ExpiresAt = expiresAt;
        }

        public void Revoke(DateTimeOffset now, Guid? replacedByTokenId = null)
        {
            if (RevokedAt != null)
            {
                return;
            }
            if (replacedByTokenId == Guid.Empty)
            {
                throw new ArgumentException("ReplacedByTokenId must not be empty", nameof(replacedByTokenId));
            }
            RevokedAt = now;
            ReplacedByTokenId = replacedByTokenId;
        }

        private static void ValidateTokenHash(string tokenHash)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(tokenHash);
            if (tokenHash.Length != TokenHashLength)
            {
                throw new ArgumentException($"TokenHash length must be {TokenHashLength}", nameof(tokenHash));
            }
        }
    }
}
