
namespace PlayerHub.Domain.Entities
{
    public class AnalyticsEvent
    {
        public const int NameMaxLength = 64;
        public const int ParametersMaxLength = 4096;

        public Guid Id { get; private set; }
        public Guid PlayerId { get; private set; }
        public string Name { get; private set; } = null!;
        public string Parameters { get; private set; } = null!;
        public DateTimeOffset ClientTime { get; private set; }
        public DateTimeOffset ServerTime { get; private set; }
        public Guid SessionId { get; private set; }

        private AnalyticsEvent() { }

        public AnalyticsEvent(
            Guid id, 
            Guid playerId, 
            string name, 
            string? parameters, 
            DateTimeOffset clientTime, 
            DateTimeOffset serverTime, 
            Guid sessionId)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Id must not be empty", nameof(id));
            }
            if (playerId == Guid.Empty)
            {
                throw new ArgumentException("PlayerId must not be empty", nameof(playerId));
            }
            if (sessionId == Guid.Empty)
            {
                throw new ArgumentException("SessionId must not be empty", nameof(sessionId));
            }
            ArgumentException.ThrowIfNullOrWhiteSpace(name);
            if (name.Length > NameMaxLength)
            {
                throw new ArgumentException($"Name is longer than {NameMaxLength}", nameof(name));
            }

            var json = string.IsNullOrWhiteSpace(parameters) ? "{}" : parameters;
            if (json.Length > ParametersMaxLength)
            {
                throw new ArgumentException($"Parameters are longer than {ParametersMaxLength}", nameof(parameters));
            }

            Id = id;
            PlayerId = playerId;
            Name = name;
            Parameters = json;
            ClientTime = clientTime;
            ServerTime = serverTime;
            SessionId = sessionId;
        }
    }
}
