using System;
using System.ComponentModel.DataAnnotations;

namespace BrgyLink.Models
{
    public class Blotter
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Incident is required.")]
        [StringLength(100, ErrorMessage = "Incident description can't be longer than 100 characters.")]
        public string Incident { get; set; }

        [Required(ErrorMessage = "Complainant's name is required.")]
        [StringLength(100, ErrorMessage = "Complainant's name can't be longer than 100 characters.")]
        public string Complainant { get; set; }

        [Required(ErrorMessage = "Respondent's name is required.")]
        [StringLength(100, ErrorMessage = "Respondent's name can't be longer than 100 characters.")]
        public string Respondent { get; set; }

        [Required(ErrorMessage = "Date reported is required.")]
        public DateTime DateReported { get; set; } = DateTime.Now;
        public DateTime LastUpdated { get; set; } = DateTime.Now;

        [StringLength(255, ErrorMessage = "Action taken description can't be longer than 255 characters.")]
        public string ActionTaken { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        public string Status { get; set; } // Example: "Resolved", "Pending"

    }
}
