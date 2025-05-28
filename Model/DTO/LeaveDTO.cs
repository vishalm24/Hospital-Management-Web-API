using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hospital_Management.Model.DTO
{
    public class LeaveDTO
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; }
        public string AdminName { get; set; }
    }
}
