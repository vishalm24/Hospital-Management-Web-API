using System.ComponentModel.DataAnnotations;

namespace Hospital_Management.Model
{
    public class Department
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public ICollection<User> Doctors { get; set; }
        public ICollection<MedicalHistory> MedicalHistories { get; set; }
    }
}
