using System.ComponentModel.DataAnnotations;

namespace user_management_dot_net_core.Dtos.Request
{
    public class RegisterRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        [MinLength(3)]
        public string Username { get; set; }

        public string Role { get; set; } = "User";
    }
}
