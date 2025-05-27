using Hospital_Management.Model.DTO;
using Hospital_Management.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management.Controllers
{
    public class DoctorController : Controller
    {
        IDoctorService _doctorService;
        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }
        [HttpGet]
        [Route("AllDoctors")]
        [Authorize(Roles = "Admin,Receptionist")]
        public async Task<IActionResult> GetAllDoctors()
        {
            return Ok(await _doctorService.GetAllDoctors());
        }
        [HttpGet]
        [Route("GetDoctorById")]
        [Authorize(Roles = "Admin,Receptionist")]
        public async Task<IActionResult> GetDoctorById(int id)
        {
            return Ok(await _doctorService.GetDoctorById(id));
        }
        [HttpPut]
        [Route("EditDoctor")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateDoctor([FromBody] DoctorUpdateDTO doctorUpdateDTO)
        {
            return Ok(await _doctorService.UpdateDoctor(doctorUpdateDTO));
        }
        [HttpDelete]
        [Route("RemoveDoctor")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            return Ok(await _doctorService.DeleteDoctor(id));
        }
    }
}
