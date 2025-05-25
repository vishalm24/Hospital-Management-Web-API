using Hospital_Management.Data;
using Hospital_Management.Exceptions;
using Hospital_Management.Model;
using Hospital_Management.Model.DTO;
using Hospital_Management.Services.IServices;
using Microsoft.EntityFrameworkCore;

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
            var doctors = await _db.Set<DoctorDTO>().FromSqlRaw("EXECUTE GetAllDoctors").ToListAsync();
            if(doctors == null)
                throw new NotFoundException("Doctors not found.");
            result.SetSeccess(doctors);
            return result;
        }
    }
}
