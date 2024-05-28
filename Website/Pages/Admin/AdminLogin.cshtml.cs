using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Website.Pages.Admin
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public IActionResult OnPost()
        {
            // Replace these with your admin credentials
            string adminUsername = "admin";
            string adminPassword = "password";

            if (Username == adminUsername && Password == adminPassword)
            {
                HttpContext.Session.SetString("IsAdminLoggedIn", "true");
                return RedirectToPage("/Admin/Admin");
            }

            ModelState.AddModelError("", "Invalid username or password");
            return Page();
        }
    }
}