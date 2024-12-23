using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class FacilityLog
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Facility Name is required.")]
    [StringLength(255, ErrorMessage = "Facility Name cannot exceed 255 characters.")]
    public string FacilityName { get; set; }

    [Required(ErrorMessage = "Action is required.")]
    [StringLength(255, ErrorMessage = "Action description cannot exceed 255 characters.")]
    public string Action { get; set; }

    public DateTime LogDate { get; set; } = DateTime.Now;
}