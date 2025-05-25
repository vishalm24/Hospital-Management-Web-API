using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Hospital_Management.Model.DTO
{
    public class DoctorDTO
    {
        public int Id { get; set; }
        public int DepartmentName { get; set; }
        public int DoctorName { get; set; }
        public string Specialization { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
