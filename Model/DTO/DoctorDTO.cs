using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Hospital_Management.Model.DTO
{
    public class DoctorDTO
    {
        public string Name { get; set; }
        public string Specialization { get; set; }
        public DateOnly JoiningDate { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string AdminName { get; set; }
    }
}
