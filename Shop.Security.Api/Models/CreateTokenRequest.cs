

namespace Shop.Security.Api.Models
{
    public class CreateTokenRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
