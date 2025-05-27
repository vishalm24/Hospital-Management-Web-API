using Hospital_Management.Model;
using Hospital_Management.Model.DTO;

namespace Hospital_Management.Services.IServices
{
    public interface IAuthService
    {
        Task<string> Login(LoginRequest model);
        Task<string> RegisterAdmin(RegisterRequest model);
        Task<string> RegisterReceptionist(RegisterRequest model);
        Task<string> RegisterDoctor(RegisterDoctorRequest model);
        Task<ResponseModel<string>> PasswordChange();
    }
}
