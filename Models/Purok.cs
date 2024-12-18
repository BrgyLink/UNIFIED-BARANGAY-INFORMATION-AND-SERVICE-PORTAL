using System.ComponentModel.DataAnnotations;

namespace BrgyLink.Models
{
    public class Purok
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(100)]
        public string? Description { get; set; }
        public int NumberOfRegisteredPeople { get; set; } // Add this property

        public ICollection<Resident> Residents { get; set; } = new List<Resident>();
    }

}
