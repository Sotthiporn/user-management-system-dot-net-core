using user_management_dot_net_core.Dtos.Request;
using user_management_dot_net_core.Dtos.Response;
using user_management_dot_net_core.Models;

namespace user_management_dot_net_core.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserResponse>> GetAllAsync();
        Task<UserResponse> GetByIdAsync(int id);
        Task UpdateAsync(int id, UpdateProfileRequest profileRequest);
        Task DeleteAsync(int id);
    }

}
