using Microsoft.AspNetCore.Identity;

namespace Website.Areas.Identity.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; } // Change the type from object to string
    }
}