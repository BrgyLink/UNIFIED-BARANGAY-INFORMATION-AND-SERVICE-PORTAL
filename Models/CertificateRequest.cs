using System.ComponentModel.DataAnnotations;

namespace BrgyLink.Models
{
    public class CertificateRequest
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string TrackingCode { get; set; }

        [Required]
        [StringLength(50)]
        public string FullName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime PickupDate { get; set; }

        [Required]
        [StringLength(50)]
        public string PaymentMethod { get; set; }

        [StringLength(50)]
        public string GcashReferenceNo { get; set; }

        [Required]
        [StringLength(50)]
        public string Purpose { get; set; }

        public decimal? Fee { get; set; } = null;

        public DateTime RequestDate { get; set; } = DateTime.UtcNow;

        public string Status { get; set; } = "Pending";

        [StringLength(15)]
        public string? PaymentContactNumber { get; set; }
    }
}
