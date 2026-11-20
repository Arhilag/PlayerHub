namespace PlayerHub.Application.Auth
{
    public class GuestLoginResponse
    {
        public Guid PlayerId { get; set; }
        public string Nickname { get; set; } = null!;
        public int Level { get; set; }
        public int Coins { get; set; }
        public bool IsNewPlayer { get; set; }
    }
}
