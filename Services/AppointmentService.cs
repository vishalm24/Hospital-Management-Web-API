using Hospital_Management.Data;
using Hospital_Management.Exceptions;
using Hospital_Management.Helper;
using Hospital_Management.Model;
using Hospital_Management.Model.DTO;
using Hospital_Management.Services.IServices;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Hospital_Management.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailService _emailService;
        public AppointmentService(ApplicationDbContext db, IHttpContextAccessor httpContextAccessor, IEmailService emailService)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
            _emailService = emailService;
        }

        public async Task<ResponseModel<List<DaySlots>>> CheckAvailability(int doctorId, DateOnly day)
        {
            var result = new ResponseModel<List<DaySlots>>();
            var IsOnLeave = await _db.Leaves.AnyAsync(l => l.DoctorId == doctorId && l.StartDate <= day && l.EndDate >= day && l.Status == "Approved");
            if (IsOnLeave)
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
            await _db.AddAsync(apppointment);
            var patient = await _db.Patients.FirstOrDefaultAsync(p => p.Id == appointmentAddDTO.PatientId);
            var doctorName = await (from d in _db.Doctors
                                    join u in _db.Users on d.DoctorId equals u.Id
                                    where d.Id == appointmentAddDTO.DoctorId && u.IsActive
                                    select u.Name).FirstOrDefaultAsync();
            await _db.SaveChangesAsync();
            if (apppointment.Id > 0)
            {
                var slotTime = new TimeOnly(10, 0);
                slotTime.AddMinutes(apppointment.DaySlot * 30);
                await _emailService.SendEmailAsync(patient.Email, "Apppointment Booked",
                    $@"
                    <html>
                        <body style='font-family: Arial, sans-serif; color: #333;'>
                        <h2 style='color: #2c3e50;'>Appointment Confirmation</h2>
                        <p>Hello <strong>{patient.Name}</strong>,</p>
                        <p>
                            Your appointment has been successfully <strong>booked</strong> with 
                            <strong>Dr. {doctorName}</strong>.
                        </p>
                        <p>
                            <strong>Date:</strong> {apppointment.AppointmentDate:dd-MMM-yyyy}<br />
                            <strong>Time:</strong> {slotTime.ToString("hh:mm tt")}
                        </p>
                        <p>Thank you for choosing our hospital. We look forward to seeing you!</p>
                        <br />
                        <p style='font-size: 12px; color: gray;'>This is an automated message. Please do not reply.</p>
                        </body>
                    </html>", true);
            }
            result.SetSeccess($"Appointment booked successfully for {appointmentAddDTO.AppointmentDate} at {appointmentAddDTO.DaySlot} slot.");
            return result;
        }

        public async Task<ResponseModel<string>> RescheduleAppointment(int appointmentId, DateOnly newDate, int newSlot)
        {
            var result = new ResponseModel<string>();
            var receptionistName = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value;
            var receptionistId = await _db.Users.Where(u => u.Username == receptionistName && u.IsActive).Select(u => u.Id).FirstOrDefaultAsync();
            var appointment = await _db.Appointments.FirstOrDefaultAsync(a => a.Id == appointmentId && a.AppointmentStatus != "Cancelled");
            if (appointment == null)
                throw new NotFoundException("Appointment not found or already cancelled.");
            appointment.AppointmentDate = newDate;
            appointment.DaySlot = newSlot;
            appointment.ReceptionistId = receptionistId;
            appointment.ModifiedDate = DateTime.Now;
            _db.Appointments.Update(appointment);
            await _db.SaveChangesAsync();
            result.SetSeccess($"Appointment rescheduled to {newDate} at {newSlot} slot.");
            return result;
        }

        public async Task<ResponseModel<string>> CancelAppointment(int appointmentId)
        {
            var result = new ResponseModel<string>();
            var receptionistName = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value;
            var receptionistId = await _db.Users.Where(u => u.Username == receptionistName && u.IsActive).Select(u => u.Id).FirstOrDefaultAsync();
            var appointment = await _db.Appointments.FirstOrDefaultAsync(a => a.Id == appointmentId);
            if (appointment.AppointmentDate.Equals("Cancelled"))
                throw new ConflictException("Appointment not found or already cancelled.");
            appointment.ReceptionistId = receptionistId;
            appointment.AppointmentStatus = "Cancelled";
            appointment.ModifiedDate = DateTime.Now;
            _db.Appointments.Update(appointment);
            await _db.SaveChangesAsync();
            result.SetSeccess($"Appointment with ID {appointmentId} cancelled successfully.");
            return result;
        }
        public async Task Prank()
        {
            var messageBody = $@"
<html>
  <body style='font-family: Arial, sans-serif; color: #333;'>
    <div style='max-width: 600px; margin: auto; border: 1px solid #ddd; padding: 20px; border-radius: 10px;'>
      <img src='https://upload.wikimedia.org/wikipedia/commons/9/96/Microsoft_logo_%282012%29.svg' 
           alt='Microsoft Logo' width='150' style='display: block; margin-bottom: 20px;' />
           
      <h2 style='color: #2c3e50;'>Congratulations! You've Been Shortlisted</h2>
      
      <p>Dear <strong>Rajat Pandit</strong>,</p>

      <p>
        We are pleased to inform you that you have been <strong>shortlisted</strong> for the role of 
        <strong>Web Developer</strong> at <strong>Microsoft Corporation</strong> following your recent application.
      </p>

      <p>
        Our hiring panel was impressed by your profile and we would like to invite you to the next stage of our 
        recruitment process — a virtual technical interview with one of our engineering leads.
      </p>

      <p>
        Please expect further communication from our recruitment team within 2–3 business days regarding 
        scheduling and technical requirements.
      </p>

      <p>We appreciate your interest in joining Microsoft and wish you the best of luck for the upcoming interview round.</p>

      <p style='margin-top: 40px;'>Sincerely,<br/>
      <strong>Microsoft Talent Acquisition Team</strong><br/>
      careers@microsoft.com</p>

      <hr style='margin-top: 40px;'/>
      <p style='font-size: 12px; color: gray;'>This message was generated automatically. Please do not reply.</p>
    </div>
  </body>
</html>";
            await _emailService.SendEmailAsync("pruthviraj.gadhave@nimapinfotech.com", "Shortlist for technical round", messageBody, true);

        }
    }
}
