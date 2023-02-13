namespace WebApplication1.Models
{
    public class LoginModel
    {
        public string email { get; set; }
        public string password { get; set; }
    }
    public class AuthenticatedResponse
    {
        public string? Token { get; set; }
    }
}
