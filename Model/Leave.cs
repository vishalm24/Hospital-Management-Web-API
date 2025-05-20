using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital_Management.Model
{
    public class Leave
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("User")]
        public int DoctorId { get; set; }
        public virtual User Doctor { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public string Reason { get; set; }
        [Required]
        public string Status { get; set; }
        [ForeignKey("User")]
        public int AdminId { get; set; }
        public virtual User Admin { get; set; }
    }
}
