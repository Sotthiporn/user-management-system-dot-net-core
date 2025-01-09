using user_management_dot_net_core.Dtos.Request;
using user_management_dot_net_core.Dtos.Response;
using user_management_dot_net_core.Exceptions;
using user_management_dot_net_core.Helpers;
using user_management_dot_net_core.Models;
using user_management_dot_net_core.Repositories.Interfaces;
using user_management_dot_net_core.Services.Interfaces;

namespace user_management_dot_net_core.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _repository;
        private readonly TokenHelper _tokenHelper;

        public AuthService(IUserRepository repository, TokenHelper tokenHelper)
        {
            _repository = repository;
            _tokenHelper = tokenHelper;
        }

        public async Task<RegisterResponse> RegisterAsync(RegisterRequest request)
        {
            var existingUser = await _repository.GetByUsernameAsync(request.Username);
            if (existingUser != null)
            {
                throw new ValidationException("Username already exists.");
            }

            var refreshToken = _tokenHelper.GenerateRefreshToken();
            var newUser = new User
            {
                Username = request.Username,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Role = request.Role,
                RefreshToken = refreshToken,
                RefreshTokenExpiry = DateTime.UtcNow.AddDays(7)
            };

            await _repository.AddAsync(newUser);

            var token = _tokenHelper.GenerateJwtToken(newUser);

            return new RegisterResponse
            {
                Success = true,
                Message = "User registered successfully.",
                Data = new RegisterData
                {
                    Token = token,
                    RefreshToken = refreshToken,
                    User = new UserResponse(newUser)
                }
            };
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var user = await _repository.GetByUsernameAsync(request.Username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                throw new NotFoundException("Invalid username or password.");
            }

            var token = _tokenHelper.GenerateJwtToken(user);
            var refreshToken = _tokenHelper.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);
            await _repository.UpdateAsync(user);

            return new LoginResponse
            {
                Success = true,
                Message = "Login successful.",
                Data = new LoginData
                {
                    Token = token,
                    RefreshToken = refreshToken,
                    User = new UserResponse(user)
                }
            };
        }

        public async Task<RefreshTokenResponse> RefreshTokenAsync(RefreshTokenRequest request)
        {
            var user = await _repository.GetByRefreshTokenAsync(request.RefreshToken);

            if (user == null || user.RefreshTokenExpiry < DateTime.UtcNow)
            {
                throw new Exceptions.UnauthorizedAccessException("Invalid or expired refresh token.");
            }

            var newToken = _tokenHelper.GenerateJwtToken(user);
            var newRefreshToken = _tokenHelper.GenerateRefreshToken();

            // Update the refresh token
            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);
            await _repository.UpdateAsync(user);

            return new RefreshTokenResponse
            {
                Success = true,
                Token = newToken,
                RefreshToken = newRefreshToken
            };
        }

    }
}
