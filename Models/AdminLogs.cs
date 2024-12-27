using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

    public class AdminLogs
    {
        [Key]
        public int id { get; set; }

        [Required]
        public string Firstname { get; set; }

        [Required(ErrorMessage ="Action cannot exceed more than 255 characters.")]
        [StringLength(255, ErrorMessage = "Action cannot exceed more than 255 characters.")]
        public string Actions { get; set; }

        [Required(ErrorMessage = "Description cannot exceed more than 255 characters.")]
        [StringLength(255, ErrorMessage = "Description cannot exceed more than 255 characters.")]
        public string Description { get; set; }

        [Required]
        public string Role { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;
    }
