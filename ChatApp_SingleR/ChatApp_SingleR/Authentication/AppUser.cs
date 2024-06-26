using Microsoft.AspNetCore.Identity;

namespace ChatApp_SingleR.Authentication
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
    }
}
