using Microsoft.AspNetCore.Identity;

namespace Dingo.Models
{
    public class AppUser:IdentityUser
    {
        public string Fullname { get; set; }
        public bool IsActivated { get; set; }
    }
}
