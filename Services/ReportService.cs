using Hospital_Management.Data;
using Hospital_Management.Model;
using Hospital_Management.Model.DTO;
using Hospital_Management.Services.IServices;
using Microsoft.EntityFrameworkCore;
using System.Net.WebSockets;

namespace Hospital_Management.Services
{
    public class ReportService: IReportService
    {
        private readonly ApplicationDbContext _db;
        public ReportService(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<ResponseModel<DocDept>> GetDailyAppointmentsByDoctor(int doctorId, DateOnly date)
        {
            var result = new ResponseModel<DocDept>();
            //var doctorCount = await _db.Appointments.Where(x => x.DoctorId == doctorId && x.AppointmentDate == date && x.AppointmentStatus.Equals("Success")).Count();
            var query = $"EXEC Get_Doctor_Depatment_Count {doctorId}, '{date:yyyy-MM-dd}'";
            var docDept = _db.DocDept.FromSqlRaw(query).AsEnumerable().FirstOrDefault();
            result.SetSeccess(docDept);
            return result;
        }
        public async Task<ResponseModel<List<PatientFequency>>> PatientVisitReport(int patientId)
        {
            var result = new ResponseModel<List<PatientFequency>>();
            var data = await _db.Appointments.Where(x => x.PatientId == patientId && x.AppointmentStatus.Equals("Success"))
                .GroupBy(x => new {x.AppointmentDate.Year, x.AppointmentDate.Month})
                .Select(g => new PatientFequency  { Year = g.Key.Year, Month = g.Key.Month, Count = g.Count() })
                .OrderBy(g => g.Year).ThenBy(g => g.Month)
                .ToListAsync();
            result.SetSeccess(data);
            return result;
        }
    }
}
