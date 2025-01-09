using user_management_dot_net_core.Dtos.Request;
using user_management_dot_net_core.Dtos.Response;
using user_management_dot_net_core.Exceptions;
using user_management_dot_net_core.Models;
using user_management_dot_net_core.Repositories.Interfaces;
using user_management_dot_net_core.Services.Interfaces;

namespace user_management_dot_net_core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<UserResponse>> GetAllAsync()
        {
            var users = await _repository.GetAllAsync();
            var userResponses = users.Select(user => new UserResponse(user)).ToList();
            return userResponses;
        }

        public async Task<UserResponse> GetByIdAsync(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            if (user == null) throw new NotFoundException("User not found");
            return new UserResponse(user);
        }

        public async Task UpdateAsync(int id, UpdateProfileRequest profileRequest)
        {
            var existingUser = await _repository.GetByIdAsync(id);
            if (existingUser == null) throw new NotFoundException("User not found");

            existingUser.Email = profileRequest.Email ?? existingUser.Email;
            existingUser.Username = profileRequest.Username ?? existingUser.Username;
            await _repository.UpdateAsync(existingUser);
        }

        public async Task DeleteAsync(int id)
        {
            var existingUser = await _repository.GetByIdAsync(id);
            if (existingUser == null) throw new NotFoundException("User not found");

            await _repository.DeleteAsync(id);
        }
    }
}
