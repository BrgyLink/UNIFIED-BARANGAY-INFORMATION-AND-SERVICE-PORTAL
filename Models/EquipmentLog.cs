using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class EquipmentLog
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Equipment Name is required.")]
    [StringLength(255, ErrorMessage = "Equipment Name cannot exceed 255 characters.")]
    public string EquipmentName { get; set; } // Changed from EquipmentId to EquipmentName

    [Required(ErrorMessage = "Action is required.")]
    [StringLength(255, ErrorMessage = "Action description cannot exceed 255 characters.")]
    public string Action { get; set; }

    public DateTime LogDate { get; set; } = DateTime.Now;
}

