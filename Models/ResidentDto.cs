using System;
using System.ComponentModel.DataAnnotations;

namespace BrgyLink.Models
{
    public class ResidentDto
    {
        public int ResidentID { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string? MiddleName { get; set; }
        public string Address { get; set; }
        public string ContactNumber { get; set; }
        public string? Email { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public string ResidencyStatus { get; set; }
        public bool IsSeniorCitizen { get; set; }
        public bool IsPWD { get; set; }
        public string Nationality { get; set; }
        public string CivilStatus { get; set; }
        public string? Occupation { get; set; }
        public string VoterStatus { get; set; }
        public DateTime DateRegistered { get; set; }
        public byte[] ImageData { get; set; }
        public string EmergencyContact { get; set; }
        public string? HealthConditions { get; set; }
    }
}
