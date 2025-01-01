using DashboardConseil.Data;
using DashboardConseil.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace DashboardConseil.Controllers
{
    [Authorize]
    public class OffresEmploiController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public OffresEmploiController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // ----------------------------
        // Liste des offres d'emploi
        // ----------------------------
        public async Task<IActionResult> IndexOffreEmploi()
        {
            var offresEmploi = await _context.OffresEmploi.ToListAsync();
            return View("~/Views/OffresEmploi/IndexOffreEmploi.cshtml", offresEmploi);
        }

        // ----------------------------
        // Formulaire de création d'une offre
        // ----------------------------
        public IActionResult CreateOffreEmploi()
        {
            return View("~/Views/OffresEmploi/CreateOffreEmploi.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOffreEmploi(OffreEmploi model)
        {
            if (ModelState.IsValid)
            {
                // Ajouter l'offre à la base de données
                _context.OffresEmploi.Add(model);
                await _context.SaveChangesAsync();

                // Rediriger vers la liste des offres
                return RedirectToAction("IndexOffreEmploi");
            }
            return View(model);
        }

        // ----------------------------
        // Formulaire de modification d'une offre
        // ----------------------------
        public async Task<IActionResult> Edit(int id)
        {
            var offre = await _context.OffresEmploi.FindAsync(id);
            if (offre == null)
            {
                return NotFound();
            }
            return View(offre);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(OffreEmploi model)
        {
            if (ModelState.IsValid)
            {
                var offre = await _context.OffresEmploi.FindAsync(model.Id);
                if (offre == null)
                {
                    return NotFound();
                }

                // Mise à jour des champs
                offre.Titre = model.Titre;
                offre.Description = model.Description;
                offre.Entreprise = model.Entreprise;
                offre.DatePublication = model.DatePublication;
                offre.Lieu = model.Lieu;
                offre.MinimumQualifications = model.MinimumQualifications;
                offre.PreferredQualifications = model.PreferredQualifications;

                _context.OffresEmploi.Update(offre);
                await _context.SaveChangesAsync();

                return RedirectToAction("IndexOffreEmploi");
            }
            return View(model);
        }

        // ----------------------------
        // Suppression d'une offre
        // ----------------------------
        public async Task<IActionResult> Delete(int id)
        {
            var offreEmploi = await _context.OffresEmploi.FindAsync(id);
            if (offreEmploi != null)
            {
                _context.OffresEmploi.Remove(offreEmploi);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(IndexOffreEmploi));
        }
        public async Task<IActionResult> Apply(int id)
        {
            var offre = await _context.OffresEmploi.FindAsync(id);
            return View(offre);
        }

        [HttpPost]
        public async Task<IActionResult> Soumettre(int id, string prenom, string nom, string email, IFormFile cv, string message)
        {

            // Process the uploaded CV file
            string cvPath = null;
            if (cv != null && cv.Length > 0)
            {
                // Define the file path where CV will be saved
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/cvs");
                Directory.CreateDirectory(uploadsFolder); // Ensure the folder exists
                cvPath = Path.Combine(uploadsFolder, $"{Guid.NewGuid()}_{cv.FileName}");

                using (var fileStream = new FileStream(cvPath, FileMode.Create))
                {
                    await cv.CopyToAsync(fileStream);
                }
            }

            // Create a new Candidature object
            var candidature = new Candidature
            {
                OffreId = id,
                Prenom = prenom,
                Nom = nom,
                Email = email,
                CvFilePath = cvPath,
                Message = message,
                DateCandidature = DateTime.Now
            };

            // Save the Candidature object to the database
            _context.Candidatures.Add(candidature);
            await _context.SaveChangesAsync();

            // Redirect to a confirmation page
           
            TempData["Message"] = "Votre candidature a été soumise avec succès.";
            var referer = Request.Headers["Referer"].ToString();
            return Redirect(referer);
        }
    }
}
