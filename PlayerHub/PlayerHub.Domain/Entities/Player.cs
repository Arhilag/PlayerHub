
using System.ComponentModel.DataAnnotations;

namespace PlayerHub.Domain.Entities
{
    public class Player
    {
        public const int DeviceIdMaxLength = 128;
        public const int NicknameMaxLength = 32;

        public Guid Id { get; private set; }
        public string DeviceId { get; private set; } = null!;
        public string Nickname { get; private set; } = null!;
        public int Level { get; private set; }
        public int Coins { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public DateTimeOffset LastSeenAt { get; private set; }
        public bool IsBanned { get; private set; }

        private Player() { }

        public Player(Guid id, string deviceId, string nickname, DateTimeOffset now)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Id must not be empty", nameof(id));
            }
            ValidateDeviceId(deviceId);
            ValidateNickname(nickname);
            ArgumentException.ThrowIfNullOrWhiteSpace(deviceId);
            ArgumentException.ThrowIfNullOrWhiteSpace(nickname);

            Id = id;
            DeviceId = deviceId;
            Nickname = nickname;
            Level = 1;
            Coins = 0;
            CreatedAt = now;
            LastSeenAt = now;
            IsBanned = false;
        }

        public void UpdateProfile(string nickname, int level, int coins)
        {
            ValidateNickname(nickname);
            ArgumentOutOfRangeException.ThrowIfLessThan(level, 1);
            ArgumentOutOfRangeException.ThrowIfNegative(coins);

            Nickname = nickname;
            Level = level;
            Coins = coins;
        }

        public void Ban()
        {
            IsBanned = true;
        }

        public void Unban()
        {
            IsBanned = false;
        }
        private static void ValidateDeviceId(string deviceId)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(deviceId);
            if (deviceId.Length > DeviceIdMaxLength)
            {
                throw new ArgumentException($"DeviceId is longer than {DeviceIdMaxLength}", nameof(deviceId));
            }
        }

        private static void ValidateNickname(string nickname)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(nickname);
            if (nickname.Length > NicknameMaxLength)
            {
                throw new ArgumentException($"Nickname is longer than {NicknameMaxLength}", nameof(nickname));
            }
        }
    }
}