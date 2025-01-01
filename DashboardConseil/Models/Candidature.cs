namespace DashboardConseil.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Candidature
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Prenom { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nom { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        
        public string? Message { get; set; }

        [Required]
        [MaxLength(200)]
        public string CvFilePath { get; set; }

        public DateTime DateCandidature { get; set; } = DateTime.Now;

        // Lié à l'offre d'emploi associée
        public int OffreId { get; set; }

        // Propriété de navigation pour l'offre
        public OffreEmploi Offre { get; set; }
    }


}
