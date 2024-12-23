using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Facility
{
    [Key]  // Marks the primary key
    public int Id { get; set; }

    [Required(ErrorMessage = "Facility Name is required.")]
    [StringLength(255, ErrorMessage = "Facility Name cannot exceed 255 characters.")]
    public string Name { get; set; }

    [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
    public string Description { get; set; }

    public DateTime LastUpdated { get; set; } = DateTime.Now;

    // Initialize collections to avoid null references during model binding
    public ICollection<Equipment> Equipments { get; set; } = new List<Equipment>();

    public ICollection<FacilityLog> FacilityLogs { get; set; } = new List<FacilityLog>();
}
