using user_management_dot_net_core.Models;

namespace user_management_dot_net_core.Dtos.Response
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

        public UserResponse(User user)
        {
            Id = user.Id;
            Username = user.Username;
            Email = user.Email;
            Role = user.Role;
        }
    }

}
