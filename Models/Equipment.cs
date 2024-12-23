using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

public class Equipment
{
    [Key]  // Marks the primary key
    public int Id { get; set; }

    [Required]
    public int FacilityId { get; set; }  // Foreign key to Facility
    public Facility? Facility { get; set; }  // Navigation property back to Facility




    [Required(ErrorMessage = "Equipment Name is required.")]
    [StringLength(255, ErrorMessage = "Equipment Name cannot exceed 255 characters.")]
    public string Name { get; set; }

    [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
    public string Description { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be a positive number.")]
    public int Quantity { get; set; }

    [Required(ErrorMessage = "Status is required.")]
    [StringLength(50, ErrorMessage = "Status cannot exceed 50 characters.")]
    [RegularExpression(@"^(In Use|Broken|Out of Stock|Reserved)$", ErrorMessage = "Invalid status. Choose from 'In Use', 'Broken', 'Out of Stock', or 'Reserved'.")]
    public string Status { get; set; }

    public DateTime LastUpdated { get; set; } = DateTime.Now;

    // Initialize EquipmentLogs as an empty collection
    public ICollection<EquipmentLog> EquipmentLogs { get; set; } = new List<EquipmentLog>();
}
