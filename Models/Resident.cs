using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrgyLink.Models
{
    public class Resident
    {

        [Key]
        public int ResidentID { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(50)]
        public string MiddleName { get; set; }

        // Foreign key for Purok
        [Required]
        public int PurokId { get; set; } // Foreign key


        // Navigation property for Purok
        public Purok? Purok { get; set; } // Navigation property

        [Required]
        [Phone]
        [StringLength(20)]
        public string? ContactNumber { get; set; }

        [EmailAddress]
        [StringLength(100)]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Required]
        [StringLength(10)]
        public string? Gender { get; set; }

        [Required]
        [StringLength(20)]
        public string ResidencyStatus { get; set; } = "Resident";

        public bool IsSeniorCitizen { get; set; }

        public bool IsPWD { get; set; }

        [Required]
        [StringLength(50)]
        public string Nationality { get; set; } = "Filipino";

        [Required]
        [StringLength(20)]
        public string CivilStatus { get; set; } = "Single";

        [StringLength(100)]
        public string? Occupation { get; set; }

        [Required]
        [StringLength(20)]
        public string VoterStatus { get; set; } = "Non-voter";

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateRegistered { get; set; }

        [Required]
        [StringLength(100)]
        public string? EmergencyContact { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }

        public byte[]? ImageData { get; set; }

        [StringLength(500)]
        public string? HealthConditions { get; set; }

        [Range(0, 150)]
        public int Age => DateTime.Today.Year - BirthDate.Year;

    }


}
