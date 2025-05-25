using Hospital_Management.Model;
using Hospital_Management.Model.DTO;

namespace Hospital_Management.Services.IServices
{
    public interface IDoctorService
    {
        public Task<ResponseModel<List<DoctorDTO>>> GetAllDoctors();
    }
}
