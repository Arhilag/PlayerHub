using PlayerHub.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace PlayerHub.Application.Auth
{
    public class GuestLoginRequest
    {
        [Required]
        [StringLength(Player.DeviceIdMaxLength)]
        public string DeviceId { get; set; } = null!;

        [StringLength(Player.NicknameMaxLength)]
        public string? Nickname { get; set; }
    }
}