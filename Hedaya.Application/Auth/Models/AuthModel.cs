namespace Hedaya.Application.Auth.Models
{
    public class AuthModel
    {
        public string Message { get; set; }
        public bool IsAuthinticated { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public List<string> Roles { get; set; }
        public string Token { get; set; }
        public DateTime ExpiresOn { get; set; }
    }
}
