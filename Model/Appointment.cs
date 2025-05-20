using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital_Management.Model
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("Patient")]
        public int PatientId { get; set; }
        public virtual Patient Patient { get; set; }
        [Required]
        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }
        public virtual Doctor Doctor { get; set; }
        [Required]
        [ForeignKey("User")]
        public int ReceptionistId { get; set; }
        public virtual User Receptionist { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        [Required]
        public DateTime AppointmentStart { get; set; }
        public DateTime AppointmentEnd { get; set; }
        [Required]
        public string AppointmentStatus { get; set; }
    }
}
