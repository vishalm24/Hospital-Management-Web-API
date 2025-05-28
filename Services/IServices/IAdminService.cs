using Hospital_Management.Model.DTO;
using Hospital_Management.Model;

namespace Hospital_Management.Services.IServices
{
    public interface IAdminService
    {
        Task<ResponseModel<string>> UpdateDoctorLeave(LeaveUpdateDTO leaveUpdateDTO);
    }
}
