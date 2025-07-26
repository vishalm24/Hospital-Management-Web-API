using Hospital_Management.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management.Controllers
{
    public class ReportController : Controller
    {
        private readonly IReportService _reportService;
        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }
        [HttpGet]
        [Route("GetApppointments/Doctor")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetDailyAppointmentsByDoctor(int doctorId, DateOnly date)
        {
            return Ok(await _reportService.GetDailyAppointmentsByDoctor(doctorId, date));
        }
        [HttpGet]
        [Route("GetPatientFequency")]
        [Authorize(Roles = "Admin, Doctor, Receptionist")]
        public async Task<IActionResult> PatientVisitReport(int patientId)
        {
            return Ok(await _reportService.PatientVisitReport(patientId));
        }
    }
}
