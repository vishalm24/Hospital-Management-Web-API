using Hospital_Management.Model;
using Hospital_Management.Model.DTO;

namespace Hospital_Management.Services.IServices
{
    public interface IPatientService
    {
        public Task<ResponseModel<List<PatientDTO>>> GetAllPatients();
        public Task<ResponseModel<PatientDTO>> GetPatientById(int id);
        public Task<ResponseModel<PatientDTO>> AddPatient(PatientAddDTO patientAddDTO);
        public Task<ResponseModel<string>> UpdatePatient(PatientDTO patientDTO);
        public Task<ResponseModel<string>> DeletePatient(int id);
        public Task<ResponseModel<PatientDTO>> SearchPatient(string search);
    }
}
