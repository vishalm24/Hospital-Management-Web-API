using Hospital_Management.Data;
using Hospital_Management.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Management.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _appointmentService;
        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Route("CheckAvailability")]
        [Authorize(Roles = "Receptionist")]
        public async Task<IActionResult> CheckAvailability(int doctorId, DateOnly day)
        {
            return Ok();
        }
        public async Task<IActionResult> BookAppointment()
        {
            return Ok();
        }
    }
}
