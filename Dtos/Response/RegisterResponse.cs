namespace user_management_dot_net_core.Dtos.Response
{
    public class RegisterResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public RegisterData Data { get; set; }

    }

    public class RegisterData
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public UserResponse User { get; set; }
    }
}
