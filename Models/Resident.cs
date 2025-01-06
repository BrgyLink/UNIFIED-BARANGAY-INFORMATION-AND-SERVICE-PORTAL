using BrgyLink.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

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
    public int PurokId { get; set; }
    // Navigation property for Purok
    public Purok? Purok { get; set; }
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
    public string ResidencyStatus { get; set; }
    public bool IsSeniorCitizen { get; set; }
    public bool IsPWD { get; set; }
    [Required]
    [StringLength(50)]
    public string Nationality { get; set; }
    [Required]
    [StringLength(20)]
    public string CivilStatus { get; set; }
    [StringLength(100)]
    public string? Occupation { get; set; }
    [Required]
    [StringLength(20)]
    public string VoterStatus { get; set; }
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
    public int Age => DateTime.Now.Year - BirthDate.Year;
}