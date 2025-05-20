using System.ComponentModel.DataAnnotations;

namespace Hospital_Management.Model
{
    public class Patient
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Phone { get; set; }
        public string? Email { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        public DateOnly BirthDate { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ActiveTreatment { get; set; }
        public bool IsActive { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
        public ICollection<MedicalHistory> MedicalHistories { get; set; }
    }
}
