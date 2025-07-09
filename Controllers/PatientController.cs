using Hospital_Management.Model;
using Hospital_Management.Model.DTO;
using Hospital_Management.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management.Controllers
{
    public class PatientController : Controller
    {
        private readonly IPatientService _patientService;
        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }
        [HttpGet]
        [Route("GetPatients")]
        [Authorize(Roles = "Admin, Doctor, Receptionist")]
        public async Task<IActionResult> GetAllPatients()
        {
            return Ok(await _patientService.GetAllPatients());
        }
        [HttpGet]
        [Route("GetPatientById/{id}")]
        [Authorize(Roles = "Admin, Doctor, Receptionist")]
        public async Task<IActionResult> GetPatientById(int id)
        {
            return Ok(await _patientService.GetPatientById(id));
        }
        [HttpPost]
        [Route("AddPatient")]
        [Authorize(Roles = "Admin, Doctor, Receptionist")]
        public async Task<IActionResult> AddPatient([FromBody] PatientAddDTO patientAddDTO)
        {
            return Ok(await _patientService.AddPatient(patientAddDTO));
        }
        [HttpPut]
        [Route("UpdatePatient")]
        [Authorize(Roles = "Admin, Doctor, Receptionist")]
        public async Task<IActionResult> UpdatePatient([FromBody] PatientUpdateDTO patientUpdateDTO)
        {
            return Ok(await _patientService.UpdatePatient(patientUpdateDTO));
        }
        [HttpGet]
        [Route("SearchPatient")]
        [Authorize(Roles = "Admin, Doctor, Receptionist")]
        public async Task<IActionResult> SearchPatient(string search)
        {
            return Ok(await _patientService.SearchPatient(search));
        }
        [HttpPost]
        [Route("AddReport")]
        [Authorize(Roles = "Admin, Receptionist")]
        public async Task<IActionResult> AddMedicalHistory([FromBody] MedicalReportAddDTO medicalReportAddDTO)
        {
            return Ok(await _patientService.AddMedicalHistory(medicalReportAddDTO));
        }
        [HttpPut]
        [Route("UpdateReport")]
        [Authorize(Roles = "Admin, Receptionist")]
        public async Task<IActionResult> UpdateMedicalHistory([FromBody] MedicalReportUpdateDTO medicalReportUpdateDTO)
        {
            return Ok(await _patientService.UpdateMedicalHistory(medicalReportUpdateDTO));
        }
        [HttpGet]
        [Route("GetHistoryByPatientId")]
        [Authorize(Roles = "Admim, Receptionist")]
        public async Task<IActionResult> GetHistoryByPatientId(int patientId)
        {
            return Ok(await _patientService.GetHistoryByPatientId(patientId));
        }
        [HttpDelete]
        [Route("RemoveHistory")]
        [Authorize(Roles = "Admin, Receptionist")]
        public async Task<IActionResult> RemoveMedicalHistory(int id)
        {
            return Ok(await _patientService.RemoveMedicalHistory(id));
        }
    }
}
