namespace OgrenciBilgiSistemiProject.DTOs
{
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginResponse
    {
        public string Token { get; set; }
        public string Role { get; set; } = default;
        public string Username { get; set; }
    }
}