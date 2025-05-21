using Hospital_Management.Data;
using Hospital_Management.Model;
using Hospital_Management.Services.IServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Hospital_Management.Services
{
    public class PatientService: IPatientService
    {
        private readonly ApplicationDbContext _db;
        public PatientService(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<ResponseModel<List<Patient>>> GetAllPatients()
        {
            var result = new ResponseModel<List<Patient>>();
            var data = await _db.Patients.ToListAsync();
            if(data == null)
                throw new FileNotFoundException("Patients not found.");
            result.SetSeccess(data);
            return result;
        }
        public async Task<ResponseModel<Patient>> GetPatientById(int id)
        {
            var result = new ResponseModel<Patient>();
            var data = await _db.Patients.FirstOrDefaultAsync(p => p.Id == id);
            if (data == null)
                throw new FileNotFoundException("Patient not found.");
            result.SetSeccess(data);
            return result;
        }
        public async Task<ResponseModel<Patient>> AddPatient(Patient patient)
        {
            var result = new ResponseModel<Patient>();
            _db.Patients.Add(patient);
            await _db.SaveChangesAsync();
            if(patient.Id == null)
            {
                throw new ApplicationException("Patient not added.");
            }
            result.SetSeccess(patient);
            return result;
        }
        public async Task<ResponseModel<Patient>> UpdatePatient(Patient patient)
        {
            var result = new ResponseModel<Patient>();
            var patientFromDb = await _db.Patients.FirstOrDefaultAsync(p => p.Id == patient.Id);
            if(patientFromDb == null)
                throw new FileNotFoundException("Patient not found.");
            patientFromDb.Name = patient.Name;
            patientFromDb.Phone = patient.Phone;
            patientFromDb.Email = patient.Email;
            patientFromDb.CreatedDate = patient.CreatedDate;
            patientFromDb.BirthDate = patient.BirthDate;
            patientFromDb.Address = patient.Address;
            patientFromDb.City = patient.City;
            patientFromDb.State = patient.State;
            patientFromDb.ActiveTreatment = patient.ActiveTreatment;
            _db.Patients.Update(patientFromDb);
            await _db.SaveChangesAsync();
            result.SetSeccess(patientFromDb);
            return result;
        }
        public async Task<ResponseModel<Patient>> DeletePatient(int id)
        {
            var result = new ResponseModel<Patient>();
            var patient = await _db.Patients.FirstOrDefaultAsync(p => p.Id == id);
            if (patient == null)
                throw new FileNotFoundException("Patient not found.");
            _db.Patients.Remove(patient);
            await _db.SaveChangesAsync();
            result.Data = patient;
            return result;
        }
    }
}
