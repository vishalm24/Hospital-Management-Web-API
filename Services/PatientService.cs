using Hospital_Management.Data;
using Hospital_Management.Exceptions;
using Hospital_Management.Model;
using Hospital_Management.Model.DTO;
using Hospital_Management.Services.IServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;

namespace Hospital_Management.Services
{
    public class PatientService: IPatientService
    {
        private readonly ApplicationDbContext _db;
        public PatientService(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<ResponseModel<List<PatientDTO>>> GetAllPatients()
        {
            var result = new ResponseModel<List<PatientDTO>>();
            var patients = await _db.Patients.Where(p => p.IsActive == true).ToListAsync();
            if(patients == null)
                throw new NotFoundException("Patients not found.");
            var data = new List<PatientDTO>();
            foreach (var item in patients)
            {
                data.Add(ShowDetails(item));
            }
            result.SetSeccess(data);
            return result;
        }
        public async Task<ResponseModel<PatientDTO>> GetPatientById(int id)
        {
            var result = new ResponseModel<PatientDTO>();
            var patient = await _db.Patients.FirstOrDefaultAsync(p => p.Id == id);
            if (patient == null)
                throw new NotFoundException($"Patient with Id: {id} not found.");
            result.SetSeccess(ShowDetails(patient));
            return result;
        }
        public async Task<ResponseModel<PatientDTO>> AddPatient(PatientAddDTO patientAddDTO)
        {
            var result = new ResponseModel<PatientDTO>();
            var patient = await _db.Patients.FirstOrDefaultAsync(p => p.Name == patientAddDTO.Name && (p.BirthDate == patientAddDTO.BirthDate || p.Phone == patientAddDTO.Phone));
            if (patient != null && patient.IsActive == true)
            {
                patient.IsActive = true;
                _db.Patients.Update(patient);
                await _db.SaveChangesAsync();
                result.SetSeccess(ShowDetails(patient));
                throw new ConflictException("Patient already exists. Please try to update details.");
            }
            else if(patient != null)
            {
                result.SetConflict(ShowDetails(patient));
                throw new ConflictException("Patient already exists.");
            }
            else
            {
                patient = new Patient
                {
                    Name = patientAddDTO.Name,
                    Phone = patientAddDTO.Phone,
                    Email = patientAddDTO.Email,
                    CreatedDate = DateTime.Now,
                    BirthDate = patientAddDTO.BirthDate,
                    Address = patientAddDTO.Address,
                    City = patientAddDTO.City,
                    State = patientAddDTO.State,
                    ActiveTreatment = patientAddDTO.ActiveTreatment,
                    IsActive = true
                };
                _db.Patients.Add(patient);
            }
            await _db.SaveChangesAsync();
            result.SetSeccess(ShowDetails(patient));
            return result;
        }
        public async Task<ResponseModel<string>> UpdatePatient(PatientDTO patientDTO)
        {
            var result = new ResponseModel<string>();
            var patient = await _db.Patients.FirstOrDefaultAsync(p => p.Id == patientDTO.Id);
            if(patient == null)
                throw new NotFoundException("Patient not found.");
            patient.Name = patientDTO.Name;
            patient.Phone = patientDTO.Phone;
            patient.Email = patientDTO.Email;
            patient.CreatedDate = patientDTO.CreatedDate;
            patient.BirthDate = patientDTO.BirthDate;
            patient.Address = patientDTO.Address;
            patient.City = patientDTO.City;
            patient.State = patientDTO.State;
            patient.ActiveTreatment = patientDTO.ActiveTreatment;
            patient.IsActive = true;
            _db.Patients.Update(patient);
            await _db.SaveChangesAsync();
            result.SetSeccess($"Patient consiting Id = {patientDTO.Id} is updated");
            return result;
        }
        public async Task<ResponseModel<string>> DeletePatient(int id)
        {
            var result = new ResponseModel<string>();
            var patient = await _db.Patients.FirstOrDefaultAsync(p => p.Id == id && p.IsActive == true);
            if (patient == null)
                throw new NotFoundException("Patient not found.");
            _db.Patients.Remove(patient);
            await _db.SaveChangesAsync();
            result.SetSeccess($"Patient with Id: {id} deleted successfully.");
            return result;
        }
        public async Task<ResponseModel<PatientDTO>> SearchPatient(string search)
        {
            var result = new ResponseModel<PatientDTO>();
            var patient = await _db.Patients.FirstOrDefaultAsync(p => p.Name.Contains(search) || p.Phone.Contains(search) || p.Email.Contains(search));
            if (patient == null)
                throw new NotFoundException("Patient not found.");
            result.SetSeccess(ShowDetails(patient));
            return result;
        }
        public PatientDTO ShowDetails(Patient patient)
        {
            return new PatientDTO
            {
                Id = patient.Id,
                Name = patient.Name,
                Phone = patient.Phone,
                Email = patient.Email,
                CreatedDate = patient.CreatedDate,
                BirthDate = patient.BirthDate,
                Address = patient.Address,
                City = patient.City,
                State = patient.State,
                ActiveTreatment = patient.ActiveTreatment
            };
        }
    }
}
