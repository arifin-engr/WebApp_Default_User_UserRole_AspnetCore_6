using Microsoft.AspNetCore.Identity;

namespace WebApp_Default_User_UserRole.Models
{
    public class AppUser:IdentityUser
    {
        // here we can use some Custom Filed its optional
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
