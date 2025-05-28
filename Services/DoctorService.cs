using Hospital_Management.Data;
using Hospital_Management.Exceptions;
using Hospital_Management.Model;
using Hospital_Management.Model.DTO;
using Hospital_Management.Services.IServices;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Hospital_Management.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly ApplicationDbContext _db;
        public DoctorService(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<ResponseModel<List<DoctorDTO>>> GetAllDoctors()
        {
            var result = new ResponseModel<List<DoctorDTO>>();
            var doctors = await _db.Set<DoctorDTO>().FromSqlRaw("EXECUTE GetAllDoctors").AsNoTracking().ToListAsync();
            if(doctors == null)
                throw new NotFoundException("Doctors not found.");
            result.SetSeccess(doctors);
            return result;
        }
        public async Task<ResponseModel<DoctorDTO>> GetDoctorById(int id)
        {
            var result = new ResponseModel<DoctorDTO>();
            var doctor = await _db.Set<DoctorDTO>().FromSqlRaw("EXECUTE GetDoctorById @Id={0}", id).FirstOrDefaultAsync();
            if (doctor == null)
                throw new NotFoundException("Doctor not found.");
            result.SetSeccess(doctor);
            return result;
        }
        public async Task<ResponseModel<string>> UpdateDoctor(DoctorUpdateDTO doctorUpdateDTO)
        {
            var result = new ResponseModel<string>();
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == doctorUpdateDTO.UserId && u.IsActive == true);
            if (user == null)
                throw new NotFoundException("User not found.");
            var doctor = await _db.Doctors.FirstOrDefaultAsync(d => d.DoctorId == doctorUpdateDTO.UserId);
            if (doctor == null)
                throw new NotFoundException("Doctor not found.");
            user.JoiningDate = doctorUpdateDTO.JoiningDate;
            user.Name = doctorUpdateDTO.Name;
            user.Email = doctorUpdateDTO.Email;
            user.Phone = doctorUpdateDTO.Phone;
            user.Address = doctorUpdateDTO.Address;
            user.City = doctorUpdateDTO.City;
            user.State = doctorUpdateDTO.State;
            doctor.Specialization = doctorUpdateDTO.Specialization;
            doctor.DepartmentId = doctorUpdateDTO.DepartmentId;
            doctor.AdminId = doctorUpdateDTO.AdminId;
            _db.Users.Update(user);
            _db.Doctors.Update(doctor);
            await _db.SaveChangesAsync();
            result.SetSeccess($"Doctor {user.Username} updated successfully.");
            return result;
        }
        public async Task<ResponseModel<string>> RemoveDoctor(int id)
        {
            var result = new ResponseModel<string>();
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == id && u.IsActive == true);
            if (user == null)
                throw new NotFoundException("User not found.");
            user.IsActive = false;
            _db.Users.Update(user);
            await _db.SaveChangesAsync();
            result.SetSeccess($"Doctor {user.Username} removed successfully.");
            return result;
        }
        public async Task<ResponseModel<LeaveDTO>> LeaveApplication(LeaveApplyDTO leaveApplyDTO)
        {
            var result = new ResponseModel<LeaveDTO>();
            var leave = new Leave
            {
                DoctorId = leaveApplyDTO.DoctorId,
                StartDate = leaveApplyDTO.StartDate,
                EndDate = leaveApplyDTO.EndDate,
                Reason = leaveApplyDTO.Reason,
                Status = "Pending",
                AdminId = leaveApplyDTO.AdminId
            };
            await _db.Leaves.AddAsync(leave);
            await _db.SaveChangesAsync();
            var leaveDTO = new LeaveDTO
            {
                Id = leave.Id,
                DoctorId = leave.DoctorId,
                StartDate = leave.StartDate,
                EndDate = leave.EndDate,
                Reason = leave.Reason,
                Status = leave.Status
            };
            leaveDTO.AdminName = await _db.Users.Where(u => u.Id == leave.AdminId).Select(u => u.Name).FirstOrDefaultAsync()??"";
            result.SetSeccess(leaveDTO);
            return result;
        }
    }
}
