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
        [Authorize(Roles = "Doctor, Receptionist")]
        public async Task<IActionResult> GetAllPatients()
        {
            return Ok(await _patientService.GetAllPatients());
        }
        [HttpGet]
        [Route("GetPatientById/{id}")]
        [Authorize(Roles = "Doctor, Receptionist")]
        public async Task<IActionResult> GetPatientById(int id)
        {
            return Ok(await _patientService.GetPatientById(id));
        }
        [HttpPost]
        [Route("AddPatient")]
        [Authorize(Roles = "Doctor, Receptionist")]
        public async Task<IActionResult> AddPatient([FromBody] PatientAddDTO patientAddDTO)
        {
            return Ok(await _patientService.AddPatient(patientAddDTO));
        }
        [HttpPut]
        [Route("UpdatePatient")]
        [Authorize(Roles = "Doctor, Receptionist")]
        public async Task<IActionResult> UpdatePatient([FromBody] PatientDTO patientUpdateDTO)
        {
            return Ok(await _patientService.UpdatePatient(patientUpdateDTO));
        }
        [HttpGet]
        [Route("SearchPatient")]
        [Authorize(Roles = "Doctor, Receptionist")]
        public async Task<IActionResult> SearchPatient(string search)
        {
            return Ok(await _patientService.SearchPatient(search));
        }
    }
}
