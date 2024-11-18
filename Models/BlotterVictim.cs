using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BrgyLink.Models
{
    public class BlotterVictim
    {
        [Key]
        public int BlotterVictimID { get; set; }

        public int BlotterID { get; set; }
        [ForeignKey("BlotterID")]
        public virtual Blotter? Blotter { get; set; }

        public int ResidentID { get; set; }
        [ForeignKey("ResidentID")]
        public virtual Resident? Victim { get; set; }
    }
}
