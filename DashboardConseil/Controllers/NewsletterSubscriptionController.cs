using DashboardConseil.Data;
using DashboardConseil.Models;
using Microsoft.AspNetCore.Mvc;

namespace DashboardConseil.Controllers
{
    public class NewsletterController : Controller
    {
        private readonly AppDbContext _context;

        public NewsletterController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Subscribe(string Email)
        {
            if (string.IsNullOrEmpty(Email) || !Email.Contains("@"))
            {
                ModelState.AddModelError("", "Please provide a valid email address.");
                return View(); // Replace with your current view.
            }

            var subscription = new NewsletterSubscription
            {
                Email = Email
            };

            _context.NewsletterSubscriptions.Add(subscription);
            await _context.SaveChangesAsync();

            TempData["Message"] = "You have successfully subscribed!";
            return RedirectToAction("Index", "Home"); // Redirect to your desired page.
        }
    }
}
