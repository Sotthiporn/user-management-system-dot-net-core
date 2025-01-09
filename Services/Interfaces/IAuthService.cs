using user_management_dot_net_core.Dtos.Request;
using user_management_dot_net_core.Dtos.Response;

namespace user_management_dot_net_core.Services.Interfaces
{
    public interface IAuthService
    {
        Task<RegisterResponse> RegisterAsync(RegisterRequest request);
        Task<LoginResponse> LoginAsync(LoginRequest request);
        Task<RefreshTokenResponse> RefreshTokenAsync(RefreshTokenRequest request);
    }
}
