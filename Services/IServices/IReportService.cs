using Hospital_Management.Model;

namespace Hospital_Management.Services.IServices
{
    public interface IReportService
    {
        Task<ResponseModel<int>> GetDailyAppointmentsByDoctor(int doctorId, DateOnly date);
    }
}
