using Hospital_Management.Model;
using Hospital_Management.Model.DTO;
using Hospital_Management.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IDoctorService _doctorService;
        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }
        [HttpGet]
        [Route("GetAllDoctors")]
        [Authorize(Roles = "Admin,Receptionist,Doctor")]
        public async Task<IActionResult> Index()
        {
            return Ok(await _doctorService.GetAllDoctors());
        }
        [HttpGet]
        [Route("GetDoctorById/{id}")]
        [Authorize(Roles = "Admin,Receptionist,Doctor")]
        public async Task<IActionResult> GetDoctorById(int id)
        {
            return Ok(await _doctorService.GetDoctorById(id));
        }
        [HttpPut]
        [Route("UpdateDoctor")]
        [Authorize(Roles = "Admin,Doctor")]
        public async Task<IActionResult> UpdateDoctor([FromBody] DoctorUpdateDTO doctorUpdateDTO)
        {
            return Ok(await _doctorService.UpdateDoctor(doctorUpdateDTO));
        }
        [HttpDelete]
        [Route("RemoveDoctor")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveDoctor(int id)
        {
            return Ok(await _doctorService.RemoveDoctor(id));
        }
        [HttpPost]
        [Route("LeaveApplication")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> LeaveApplication([FromBody] LeaveApplyDTO leaveApplyDTO)
        {
            return Ok(await _doctorService.LeaveApplication(leaveApplyDTO));
        }
    }
}
