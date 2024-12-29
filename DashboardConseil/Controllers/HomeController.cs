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
    .OrderByDescending(s => s.Id) // Trier par ID d�croissant
    .Take(3) // R�cup�rer les 3 premiers
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

        public IActionResult TeamDetaille()
        {
            return View(); // Maps to TeamDetaille.cshtml
        }
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
            return View();
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



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
