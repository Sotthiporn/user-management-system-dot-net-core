namespace user_management_dot_net_core.Dtos.Response
{
    public class RefreshTokenResponse
    {
        public bool Success { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
