using Hospital_Management.Model;
using Hospital_Management.Model.DTO;

namespace Hospital_Management.Services.IServices
{
    public interface IReportService
    {
        Task<ResponseModel<DocDept>> GetDailyAppointmentsByDoctor(int doctorId, DateOnly date);
        Task<ResponseModel<List<PatientFequency>>> PatientVisitReport(int patientId);
    }
}
