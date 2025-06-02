using Hospital_Management.Data;
using Hospital_Management.Exceptions;
using Hospital_Management.Model;
using Hospital_Management.Model.DTO;
using Hospital_Management.Services.IServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Components.Forms;

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
        public async Task<ResponseModel<MedicalHistory>> AddMedicalHistory(MedicalReportAddDTO medicalReportAddDTO)
        {
            var result = new ResponseModel<MedicalHistory>();
            var patient = await _db.Patients.FirstOrDefaultAsync(p => p.Id == medicalReportAddDTO.PatientId && p.IsActive == true);
            if (patient == null)
                throw new NotFoundException("Patient not found.");
            var medicalHistory = new MedicalHistory();
            medicalHistory.PatientId = medicalReportAddDTO.PatientId;
            medicalHistory.DoctorId = medicalReportAddDTO.DoctorId;
            medicalHistory.DepartmentId = medicalReportAddDTO.DepartmentId;
            medicalHistory.Symptoms = medicalReportAddDTO.Symptoms;
            medicalHistory.Diagnose = medicalReportAddDTO.Diagnose;
            medicalHistory.Treatments = medicalReportAddDTO.Treatments;
            medicalHistory.PastContactedDate = medicalReportAddDTO.PastContactedDate;
            medicalHistory.CreatedDate = DateTime.Now;
            medicalHistory.ModifiedDate = DateTime.Now;
            _db.MedicalHistories.Add(medicalHistory);
            await _db.SaveChangesAsync();
            result.SetSeccess(medicalHistory);
            return result;
        }
        public async Task<ResponseModel<string>> UpdateMedicalHistory(MedicalReportUpdateDTO medicalReportUpdateDTO)
        {
            var result = new ResponseModel<string>();
            var medicalHistory = await _db.MedicalHistories.FirstOrDefaultAsync(m => m.Id == medicalReportUpdateDTO.Id);
            medicalHistory.DoctorId = medicalReportUpdateDTO.DoctorId;
            medicalHistory.DepartmentId = medicalReportUpdateDTO.DepartmentId;
            medicalHistory.Symptoms = medicalReportUpdateDTO.Symptoms;
            medicalHistory.Diagnose = medicalReportUpdateDTO.Treatments;
            _db.MedicalHistories.Update(medicalHistory);
            await _db.SaveChangesAsync();
            result.SetSeccess($"{medicalHistory.Id} is updated Sucessfully.");
            return result;
        }
        public async Task<ResponseModel<List<MedicalHistory>>> GetHistoryByPatientId(int patientId)
        {
            var result = new ResponseModel<List<MedicalHistory>>();
            var medicalHistories = await _db.MedicalHistories.Where(m => m.PatientId == patientId).ToListAsync();
            if(medicalHistories == null)
                throw new NotFoundException("Medical history not found for this patient.");
            result.SetSeccess(medicalHistories);
            return result;
        }
        public async Task<ResponseModel<string>> RemoveMedicalHistory(int Id)
        {
            var result = new ResponseModel<string>();
            var medicalHistory = await _db.MedicalHistories.FirstOrDefaultAsync(m => m.Id == Id);
            if (medicalHistory == null)
                throw new NotFoundException("Medical history not found.");
            _db.MedicalHistories.Remove(medicalHistory);
            await _db.SaveChangesAsync();
            result.SetSeccess($"Medical history with Id: {Id} deleted successfully.");
            return result;
        }
    }
}
