namespace user_management_dot_net_core.Dtos.Response
{
    public class LoginResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public LoginData Data { get; set; }

    }

    public class LoginData
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public UserResponse User { get; set; }
    }
}
