using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Hospital_Management.Model
{
    public class MedicalHistory
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("Patient")]
        public int PatientId { get; set; }
        public virtual Patient Patient { get; set; }
        [ForeignKey("User")]
        public int DoctorId { get; set; }
        public virtual User Doctor { get; set; }
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
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
