using Hospital_Management.Data;
using Hospital_Management.Exceptions;
using Hospital_Management.Model;
using Hospital_Management.Model.DTO;
using Hospital_Management.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Net;
using System.Net.WebSockets;
using System.Numerics;
using System.Security.Claims;

namespace Hospital_Management.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DoctorService(ApplicationDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ResponseModel<List<DoctorDTO>>> GetAllDoctors()
        {
            var result = new ResponseModel<List<DoctorDTO>>();
            var doctors = await _db.Set<DoctorDTO>().FromSqlRaw("EXECUTE GetAllDoctors").ToListAsync();
            if (doctors == null)
                throw new NotFoundException("Doctors not found.");
            result.SetSeccess(doctors);
            return result;
        }
        public async Task<ResponseModel<DoctorDTO>> GetDoctorById(int id)
        {
            var result = new ResponseModel<DoctorDTO>();
            var doctor = await _db.Set<DoctorDTO>().FromSqlRaw("Execute GetDoctorById", id).FirstOrDefaultAsync();
            if(doctor == null)
                throw new NotFoundException("Doctor not found.");
            result.SetSeccess(doctor);
            return result;
        }
        public async Task<ResponseModel<string>> UpdateDoctor(DoctorUpdateDTO doctorUpdateDTO)
        {
            var result = new ResponseModel<string>();
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == doctorUpdateDTO.UserId);
            if(user == null)
                throw new NotFoundException("User not found.");
            var adminName = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value;
            var admin = await _db.Users.FirstOrDefaultAsync(u => u.Username == adminName);
            if (admin == null)
                throw new ForbiddenException("You are not authorized to update a doctor account.");
            user.Name = doctorUpdateDTO.Name;
            user.Email = doctorUpdateDTO.Email;
            user.Phone = doctorUpdateDTO.Phone;
            user.Address = doctorUpdateDTO.Address;
            user.City = doctorUpdateDTO.City;
            user.State = doctorUpdateDTO.State;
            user.AdminId = admin.Id;

            var doctor = await _db.Doctors.FirstOrDefaultAsync(d => d.Id == doctorUpdateDTO.Id);
            if(doctor == null)
                throw new NotFoundException("Doctor not found.");
            doctor.DepartmentId = doctorUpdateDTO.DepartmentId;
            doctor.AdminId = admin.Id;
            doctor.Specialization = doctorUpdateDTO.Specialization;

            _db.Users.Update(user);
            _db.Doctors.Update(doctor);
            await _db.SaveChangesAsync();
            result.SetSeccess($"{user.Username}'s details has been updated.");
            return result;
        }
        public async Task<ResponseModel<string>>DeleteDoctor(int id)
        {
            var result = new ResponseModel<string>();
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == id && u.IsActive);
            if(user == null)
                throw new NotFoundException("User not found.");
            user.IsActive = false;
            _db.Users.Update(user);
            await _db.SaveChangesAsync();
            result.SetSeccess($"{user.Username} has been deleted successfully.");
            return result;
        }
    }
}
