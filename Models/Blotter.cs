using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BrgyLink.Enums;

namespace BrgyLink.Models
{
    public class Blotter
    {
        [Key]
        public int BlotterID { get; set; }

        [Required]
        public int ComplainantID { get; set; }
        [ForeignKey("ComplainantID")]
        public virtual Resident? Complainant { get; set; }

        [Required]
        public int RespondentID { get; set; }
        [ForeignKey("RespondentID")]
        public virtual Resident? Respondent { get; set; }

        public virtual ICollection<BlotterVictim>? Victims { get; set; }

        [Required]
        public BlotterType Type { get; set; }  // Updated to use enum

        [Required]
        [StringLength(200)]
        public string? Location { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public DateTime Time { get; set; }

        [Required]
        public BlotterStatus Status { get; set; } = BlotterStatus.Pending;  // Updated to use enum

        [Required]
        [StringLength(1000)]
        public string? Description { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateFiled { get; set; } = DateTime.Now;
    }

}
