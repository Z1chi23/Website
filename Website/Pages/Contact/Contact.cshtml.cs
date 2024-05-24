using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Website.Models;
using Website.Data;

namespace Website.Pages.Contact
{
    public class ContactModel : PageModel
    {
        private readonly AppDBContext _context;

        public ContactModel(AppDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Contact ContactForm { get; set; }

        public string SuccessMessage { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                // Save the contact form message to the database
                _context.Contacts.Add(ContactForm);
                _context.SaveChanges();

                SuccessMessage = "Your message has been sent!";
                ModelState.Clear();
                return Page();
            }

            return Page();
        }
    }
}