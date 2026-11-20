namespace PlayerHub.Application.Auth
{
    public interface IAuthService
    {
        Task<GuestLoginResponse> LoginGuestAsync(GuestLoginRequest request, CancellationToken ct);
    }
}
