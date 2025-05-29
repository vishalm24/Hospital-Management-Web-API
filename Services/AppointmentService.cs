using Hospital_Management.Data;
using Hospital_Management.Exceptions;
using Hospital_Management.Helper;
using Hospital_Management.Model;
using Hospital_Management.Model.DTO;
using Hospital_Management.Services.IServices;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Hospital_Management.Services
{
    public class AppointmentService: IAppointmentService
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AppointmentService(ApplicationDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ResponseModel<List<DaySlots>>> GetAllAppointments(int doctorId, DateOnly day)
        {
            var result = new ResponseModel<List<DaySlots>>();
            var IsOnLeave = await _db.Leaves.AnyAsync(l => l.DoctorId == doctorId && l.StartDate <= day && l.EndDate >= day && l.Status == "Approved");
            if(IsOnLeave)
                throw new ConflictException("Doctor is on leave for the selected date.");
            var appointments = await _db.Appointments.Where(a => a.DoctorId == doctorId && a.AppointmentDate == day && a.AppointmentStatus == "Booked").ToListAsync();
            var daySlots = DaySlotsData.GetTimeSlots();
            foreach (var item in daySlots)
            {
                if (appointments.Any(a => a.DaySlot == item.Slot))
                    item.IsBooked = true;
            }
            result.SetSeccess(daySlots);
            return result;
        }

        public async Task<ResponseModel<string>> BookAppointment(AppointmentAddDTO appointmentAddDTO)
        {
            var result = new ResponseModel<string>();
            var receptionistName = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value;
            var receptionistId = await _db.Users.Where(u => u.Username == receptionistName && u.IsActive).Select(u => u.Id).FirstOrDefaultAsync();
            if (receptionistId == 0)
                throw new NotFoundException("Receptionist not found.");
            var IsBooked = await _db.Appointments.AnyAsync(a => a.DoctorId == appointmentAddDTO.DoctorId && a.AppointmentDate == appointmentAddDTO.AppointmentDate && a.DaySlot == appointmentAddDTO.DaySlot && a.AppointmentStatus == "Booked");
            if (IsBooked)
                throw new ConflictException("This slot is already booked. Please choose another slot.");
            var apppointment = new Appointment
            {
                PatientId = appointmentAddDTO.PatientId,
                DoctorId = appointmentAddDTO.DoctorId,
                ReceptionistId = receptionistId,
                AppointmentDate = appointmentAddDTO.AppointmentDate,
                CreatedDate = DateTime.Now,
                ModifiedDate = null,
                DaySlot = appointmentAddDTO.DaySlot,
                AppointmentStatus = "Booked",
            };
            result.SetSeccess($"Appointment booked successfully for {appointmentAddDTO.AppointmentDate} at {appointmentAddDTO.DaySlot} slot.");
            return result;
        }

    }
}
