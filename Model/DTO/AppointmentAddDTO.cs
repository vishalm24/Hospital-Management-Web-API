using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hospital_Management.Model.DTO
{
    public class AppointmentAddDTO
    {
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        //public int ReceptionistId { get; set; }
        //public DateTime CreatedDate { get; set; }
        //public DateTime? ModifiedDate { get; set; }
        public DateOnly AppointmentDate { get; set; }
        public int DaySlot { get; set; }
        //public string AppointmentStatus { get; set; }
    }
}
