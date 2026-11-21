using Microsoft.AspNetCore.Mvc;
using PlayerHub.Application.Auth;

namespace PlayerHub.Web.Controllers.Api
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("guest")]
        public async Task<ActionResult<GuestLoginResponse>> Guest([FromBody] GuestLoginRequest request, CancellationToken ct)
        {
            try
            {
                var response = await _authService.LoginGuestAsync(request, ct);
                return Ok(response);
            }
            catch (PlayerBannedException)
            {
                return StatusCode(StatusCodes.Status403Forbidden);
            }
        }
    }
}
