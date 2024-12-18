using System.ComponentModel.DataAnnotations;

namespace BrgyLink.Models
{
    public class Committee
    {
        [Key]
        public int CommitteeId { get; set; }

        [Required(ErrorMessage = "Committee name is required.")]
        [StringLength(100, ErrorMessage = "Committee name cannot be longer than 100 characters.")]
        public string CommitteeName { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Committee description cannot be longer than 500 characters.")]
        public string CommitteeDescription { get; set; } = string.Empty;

        // Navigation property for Many-to-Many relationship
        public ICollection<BarangayOfficialCommittee> BarangayOfficialCommittees { get; set; } = new List<BarangayOfficialCommittee>();
    }
}
