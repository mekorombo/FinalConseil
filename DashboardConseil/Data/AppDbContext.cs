using DashboardConseil.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DashboardConseil.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<OffreEmploi> OffresEmploi { get; set; } // Ajout du DbSet pour les offres d'emploi
        public DbSet<NewsletterSubscription> NewsletterSubscriptions { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Service> Services { get; set; }



    }
}
