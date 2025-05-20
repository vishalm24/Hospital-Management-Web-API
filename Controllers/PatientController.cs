using Hospital_Management.Model;
using Hospital_Management.Services.IServices;
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
        public async Task<IActionResult> GetAllPatients()
        {
            var result = await _patientService.GetAllPatients();
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetPatientById(int id)
        {
            var result = await _patientService.GetPatientById(id);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> AddPatient([FromBody] Patient patient)
        {
            var result = await _patientService.AddPatient(patient);
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> UpdatePatient([FromBody] Patient patient)
        {
            var result = await _patientService.UpdatePatient(patient);
            return Ok(result);
        }
    }
}
