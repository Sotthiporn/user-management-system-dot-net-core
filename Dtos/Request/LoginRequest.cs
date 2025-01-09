using System.ComponentModel.DataAnnotations;

namespace user_management_dot_net_core.Dtos.Request
{
    public class LoginRequest
    {
        [Required]
        [MinLength(3)]
        public string Username { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
}
