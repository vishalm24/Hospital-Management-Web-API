using Hospital_Management.Data;
using Hospital_Management.Model;
using Hospital_Management.Services.IServices;

namespace Hospital_Management.Services
{
    public class ReportService: IReportService
    {
        private readonly ApplicationDbContext _db;
        public ReportService(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<ResponseModel<int>> GetDailyAppointmentsByDoctor(int doctorId, DateOnly date)
        {
            var result = new ResponseModel<int>();
            var count = _db.Appointments.Where(x => x.DoctorId == doctorId && x.AppointmentDate == date && x.AppointmentStatus.Equals("Success")).Count();
            result.SetSeccess(count);
            return result;
        }
        //public async Task<ResponseModel<float>> GetDoctorUtilization(int doctorId)
        //{
        //    var result = new ResponseModel<float>();
            
        //    return result;
        //}
    }
}
