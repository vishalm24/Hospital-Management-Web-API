using Hospital_Management.Data;
using Hospital_Management.Model.DTO;
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
            return Ok(await _appointmentService.CheckAvailability(doctorId, day));
        }
        [HttpPost]
        [Route("BookAppointment")]
        [Authorize(Roles = "Receptionist")]
        public async Task<IActionResult> BookAppointment(AppointmentAddDTO appointmentAddDTO)
        {
            return Ok(await _appointmentService.BookAppointment(appointmentAddDTO));
        }
        [HttpPatch]
        [Route("RescheduleAppointment")]
        [Authorize(Roles = "Receptionist")]
        public async Task<IActionResult> RescheduleAppointment(int appointmentId, DateOnly newDate, int newSlot)
        {
            return Ok(await _appointmentService.RescheduleAppointment(appointmentId, newDate, newSlot));
        }
        [HttpDelete]
        [Route("CancelAppointment")]
        [Authorize(Roles = "Receptionist")]
        public async Task<IActionResult> CancelAppointment(int appointmentId)
        {
            return Ok(await _appointmentService.CancelAppointment(appointmentId));
        }
    }
}
