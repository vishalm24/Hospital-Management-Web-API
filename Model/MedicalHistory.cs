using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Hospital_Management.Model
{
    public class MedicalHistory
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("Patient")]
        public int PatientId { get; set; }
        [JsonIgnore]
        public virtual Patient Patient { get; set; }
        [ForeignKey("Appointment")]
        public int AppointmentId { get; set; }
        [JsonIgnore]
        public virtual Appointment Appointment { get; set; }
        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }
        [JsonIgnore]
        public virtual Doctor Doctor { get; set; }
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        [JsonIgnore]
        public virtual Department Department { get; set; }
        [Required]
        public string Symptoms { get; set; }
        public string Diagnose { get; set; }
        public string Treatments { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        public DateTime ModifiedDate { get; set; }
        public DateTime PastContactedDate { get; set; }
    }
}
