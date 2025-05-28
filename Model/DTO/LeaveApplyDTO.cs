using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hospital_Management.Model.DTO
{
    public class LeaveApplyDTO
    {
        [Required]
        public int DoctorId { get; set; }
        [Required]
        public DateOnly StartDate { get; set; }
        [Required]
        public DateOnly EndDate { get; set; }
        [Required]
        public string Reason { get; set; }
        public int AdminId { get; set; }
    }
}
