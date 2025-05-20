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
        public DateOnly JoiningDate { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        public bool IsActive { get; set; }
        [Required]
        [ForeignKey("User")]
        public int AdminId { get; set; }
        public virtual User Admin { get; set; }
        public ICollection<User> UserAdminIds { get; set; }
        public ICollection<Leave> DoctorAdminIds { get; set; }
        public ICollection<Doctor> Users { get; set; }
        public ICollection<Patient> Patients { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    }
}
