using System.ComponentModel.DataAnnotations;

namespace Hospital_Management.Model.DTO
{
    public class PatientAddDTO
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string? Email { get; set; }
        public DateOnly BirthDate { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ActiveTreatment { get; set; }
    }
}
