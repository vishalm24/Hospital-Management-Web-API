using System.ComponentModel.DataAnnotations;

namespace Hospital_Management.Model.DTO
{
    public class PatientDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string? Email { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateOnly BirthDate { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ActiveTreatment { get; set; }
    }
}
