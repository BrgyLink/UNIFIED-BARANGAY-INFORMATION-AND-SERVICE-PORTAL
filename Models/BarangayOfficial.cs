using BrgyLink.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class BarangayOfficial
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "First name is required.")]
    [StringLength(100, ErrorMessage = "First name cannot be longer than 100 characters.")]
    public string FirstName { get; set; }

    [StringLength(100, ErrorMessage = "Middle name cannot be longer than 100 characters.")]
    public string MiddleName { get; set; }

    [Required(ErrorMessage = "Last name is required.")]
    [StringLength(100, ErrorMessage = "Last name cannot be longer than 100 characters.")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Birthdate is required.")]
    [DataType(DataType.Date)]
    public DateTime? Birthdate { get; set; }

    [Required(ErrorMessage = "Gender is required.")]
    [StringLength(10, ErrorMessage = "Gender cannot be longer than 10 characters.")]
    public string Gender { get; set; }

    [Required(ErrorMessage = "Marital status is required.")]
    [StringLength(20, ErrorMessage = "Marital status cannot be longer than 20 characters.")]
    public string MaritalStatus { get; set; }

    [Required(ErrorMessage = "Barangay position is required.")]
    [StringLength(100, ErrorMessage = "Barangay position cannot be longer than 100 characters.")]
    public string BarangayPosition { get; set; }

    [Required(ErrorMessage = "Term start date is required.")]
    [DataType(DataType.Date)]
    public DateTime? TermStart { get; set; }

    [Required(ErrorMessage = "Term end date is required.")]
    [DataType(DataType.Date)]
    [TermEndValidation(ErrorMessage = "Term end must be after the term start date.")]
    public DateTime? TermEnd { get; set; }

    public int? RankNo { get; set; }

    [Required(ErrorMessage = "Status is required.")]
    [StringLength(20, ErrorMessage = "Status cannot be longer than 20 characters.")]
    public string Status { get; set; }

    // For the uploaded photo file (not stored in database)
    [NotMapped]
    public IFormFile? ImageFile { get; set; }

    // The byte array to store the photo data in the database
    public byte[]? Photo { get; set; }

    // Navigation property for Many-to-Many relationship through BarangayOfficialCommittee
    public ICollection<BarangayOfficialCommittee> BarangayOfficialCommittees { get; set; } = new List<BarangayOfficialCommittee>();

    // Computed FullName property for display in the dropdown
    public string FullName => $"{FirstName} {LastName}";
}


public class TermEndValidationAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var termEnd = value as DateTime?;
        var termStart = (validationContext.ObjectInstance as BarangayOfficial)?.TermStart;

        if (termEnd.HasValue && termStart.HasValue)
        {
            if (termEnd.Value < termStart.Value)
            {
                return new ValidationResult("Term end must be after the term start date.");
            }
        }
        return ValidationResult.Success;
    }
}
