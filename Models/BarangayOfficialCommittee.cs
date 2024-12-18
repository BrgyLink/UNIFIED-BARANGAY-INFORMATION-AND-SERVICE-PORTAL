using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrgyLink.Models
{
    public class BarangayOfficialCommittee
    {
        [Required(ErrorMessage = "The Barangay Official is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid Barangay Official")]
        public int BarangayOfficialId { get; set; }

        [Required(ErrorMessage = "The Committee is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid Committee")]
        public int CommitteeId { get; set; }

        public BarangayOfficial? BarangayOfficial { get; set; }
        public Committee? Committee { get; set; }
    }
}
