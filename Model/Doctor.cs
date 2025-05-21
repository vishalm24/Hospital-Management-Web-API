using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hospital_Management.Model
{
    public class Doctor
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }
        [ForeignKey("User")]
        public int DoctorId { get; set; }
        public virtual User user { get; set; }
        [ForeignKey("User")]
        public int AdminId { get; set; }
        public virtual User Admin { get; set; }
        public ICollection<Appointment> Appointments { get; set;}
        public ICollection<Leave> Leaves { get; set; }
        public ICollection<MedicalHistory> MedicalHistories { get; set; }
    }
}
