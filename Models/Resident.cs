namespace BrgyLink.Models
{
    public class Resident
    {
        public int ResidentID { get; set; }
        public string FirstName { get; set; }
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
        public string ProfilePicture { get; set; }
        public string EmergencyContact { get; set; }
        public byte[] ImageData { get; set; } // Store image as byte array
        public string? HealthConditions { get; set; }
        public int Age => DateTime.Today.Year - BirthDate.Year;
    }
}
