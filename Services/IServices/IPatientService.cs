using Hospital_Management.Model;

namespace Hospital_Management.Services.IServices
{
    public interface IPatientService
    {
        public Task<ResponseModel<List<Patient>>> GetAllPatients();
        public Task<ResponseModel<Patient>> GetPatientById(int id);
        public Task<ResponseModel<Patient>> AddPatient(Patient patient);
        public Task<ResponseModel<Patient>> UpdatePatient(Patient patient);
        public Task<ResponseModel<Patient>> DeletePatient(int id);
    }
}
