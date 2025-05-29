using Hospital_Management.Helper;
using Hospital_Management.Model;
using Hospital_Management.Model.DTO;

namespace Hospital_Management.Services.IServices
{
    public interface IAppointmentService
    {
        Task<ResponseModel<List<DaySlots>>> GetAllAppointments(int doctorId, DateOnly day);
        Task<ResponseModel<string>> BookAppointment(AppointmentAddDTO appointmentAddDTO);
    }
}
