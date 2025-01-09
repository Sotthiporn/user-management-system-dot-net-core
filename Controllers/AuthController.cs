using Microsoft.AspNetCore.Mvc;
using user_management_dot_net_core.Dtos.Request;
using user_management_dot_net_core.Services.Interfaces;
using user_management_dot_net_core.Exceptions;

namespace user_management_dot_net_core.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            try
            {
                var response = await _service.RegisterAsync(request);
                return Ok(response);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            try
            {
                var response = await _service.LoginAsync(request);
                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                return Unauthorized(new { Success = false, Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(RefreshTokenRequest request)
        {
            try
            {
                var response = await _service.RefreshTokenAsync(request);
                return Ok(response);
            }
            catch (Exceptions.UnauthorizedAccessException ex)
            {
                return Unauthorized(new { Success = false, Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }

    }
}
