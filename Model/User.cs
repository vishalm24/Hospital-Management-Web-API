using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital_Management.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Role { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public bool IsActive { get; set; }
        [Required]
        public int JoiningDate { get; set; }
        [Required]
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }
        [Required]
        [ForeignKey("User")]
        public int AdminId { get; set; }
        public virtual User Admin { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<Patient> Patients { get; set; }
        public ICollection<Appointment> DoctorAppointments { get; set; }
        public ICollection<Appointment> ReceptionistAppointments { get; set; }
        public ICollection<MedicalHistory> MedicalHistories { get; set; }
        public ICollection<Leave> Leaves { get; set; }
        public ICollection<Leave> AdminIds { get; set; }
    }
}
