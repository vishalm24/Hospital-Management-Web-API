using Hospital_Management.Model;
using Hospital_Management.Model.DTO;

namespace Hospital_Management.Services.IServices
{
    public interface IDoctorService
    {
        Task<ResponseModel<List<DoctorDTO>>> GetAllDoctors();
        Task<ResponseModel<DoctorDTO>> GetDoctorById(int id)
        Task<ResponseModel<string>> UpdateDoctor(DoctorUpdateDTO doctorUpdateDTO);
        Task<ResponseModel<string>> DeleteDoctor(int id);
    }
}
