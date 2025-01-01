using System.Diagnostics;
using DashboardConseil.Data;
using DashboardConseil.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DashboardConseil.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger,AppDbContext context)
        {
            _logger = logger;
            _context = context;

        }

        public async Task<IActionResult> Index()
        {

            var topServices = await _context.Services
            .OrderByDescending(s => s.Id) // Trier par ID décroissant
            .Take(3) // Récupérer les 3 premiers
            .ToListAsync();

            return View(topServices); // Maps to Team.cshtml
        }

        public IActionResult About()
        {
            return View(); // Maps to About.cshtml
        }

        public IActionResult Histoire()
        {
            return View(); // Maps to Histoire.cshtml
        }

        public IActionResult Team()
        {
            return View(); // Maps to Team.cshtml
        }

        //public IActionResult TeamDetaille()
        //{
        //    return View(); // Maps to TeamDetaille.cshtml
        //}
        public async Task<IActionResult> Service()
        {
            var services = await _context.Services.ToListAsync();
            return View(services);
        }

        public IActionResult ServiceDetaille()
        {
            return View(); // Maps to TeamDetaille.cshtml
        }

        public IActionResult Contact()
        {
            return View(); // Maps to TeamDetaille.cshtml
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize]
        public IActionResult Dashboard()
        {
            int currentYear = DateTime.Now.Year;

            // Créer une liste de tous les mois (1 à 12)
            var months = Enumerable.Range(1, 12);

            // Utiliser LINQ pour compter les abonnements par mois
            var monthlySubscriptions = months
        .GroupJoin(
            _context.NewsletterSubscriptions
                .Where(ns => ns.SubscribedOn.Year == currentYear), // Filtrer par l'année actuelle
            month => month,                                     // Clé externe : Mois de la liste
            ns => ns.SubscribedOn.Month,                       // Clé interne : Mois des abonnements
            (month, subscriptions) => new
            {
                Month = month,
                SubscriptionCount = subscriptions.Count()
            }
        )
        .OrderBy(result => result.Month) // Trier par mois
        .ToList();

            var monthlyOffers = months
                        .GroupJoin(
                            _context.OffresEmploi
                                .Where(o => o.DatePublication.Year == currentYear), // Filtrer par année
                            month => month,                                        // Mois de la liste
                            o => o.DatePublication.Month,                         // Mois des offres
                            (month, offers) => new
                            {
                                Month = month,
                                OfferCount = offers.Count()
                            }
                        )
                        .OrderBy(result => result.Month)
                        .ToList();

            // Retourner les données à la vue sous forme de tuple
            return View((monthlySubscriptions, monthlyOffers));
        }

        public IActionResult SignIn()
        {
            return View();
        }
        public IActionResult SignUp()
        {
            return View();
        }
        public IActionResult Profil()
        {
            return View();
        }

        
        public async Task<IActionResult> Offre()
        {
            var offres = await _context.OffresEmploi.ToListAsync();
            return View(offres);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
