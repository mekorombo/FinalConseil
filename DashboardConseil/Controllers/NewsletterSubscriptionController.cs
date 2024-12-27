using DashboardConseil.Data;
using DashboardConseil.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DashboardConseil.Controllers
{
    public class NewsletterController : Controller
    {
        private readonly AppDbContext _context;

        public NewsletterController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.NewsletterSubscriptions.ToListAsync());
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

        // GET: NewsletterSubscription/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subscription = _context.NewsletterSubscriptions
                .FirstOrDefault(m => m.Id == id);

            if (subscription == null)
            {
                return NotFound();
            }

            return View(subscription);
        }

        // POST: NewsletterSubscription/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var subscription = _context.NewsletterSubscriptions.Find(id);

            if (subscription != null)
            {
                _context.NewsletterSubscriptions.Remove(subscription);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
