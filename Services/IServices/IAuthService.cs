using Hospital_Management.Model;
using Hospital_Management.Model.DTO;

namespace Hospital_Management.Services.IServices
{
    public interface IAuthService
    {
        public Task<string> Login(LoginRequest model);
        //public Task<string> RegisterFirstAdmin(RegisterRequest model);
        public Task<string> RegisterAdmin(RegisterRequest model);
        public Task<string> RegisterReceptionist(RegisterRequest model);
        public Task<string> RegisterDoctor(RegisterDoctorRequest model);
    }
}
