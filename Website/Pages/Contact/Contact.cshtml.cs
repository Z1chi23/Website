using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Mail;
using Website.Models;
using Website.Data;

namespace Website.Pages.Contact
{
    public class ContactModel : PageModel
    {
        private readonly AppDBContext _context; // Declaring _context as a private field

        public ContactModel(AppDBContext context)
        {
            _context = context; // Assigning the injected AppDBContext to _context
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
                var message = new MailMessage();
                message.To.Add(new MailAddress("recipient@example.com"));  // Replace with actual recipient email address
                message.From = new MailAddress(ContactForm.Email);
                message.Subject = "Contact Form Message";
                message.Body = $"Name: {ContactForm.Name}\nEmail: {ContactForm.Email}\nMessage: {ContactForm.Message}";

                using (var smtpClient = new SmtpClient("smtp.example.com"))  // Replace with actual SMTP server
                {
                    smtpClient.Send(message);
                }

                SuccessMessage = "Your message has been sent!";
                ModelState.Clear();
                return Page();
            }

            return Page();
        }
    }
}