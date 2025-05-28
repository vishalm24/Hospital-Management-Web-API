using Hospital_Management.Model.DTO;
using Hospital_Management.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }
        [HttpGet]
        [Route("GetPendingLeaves")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetPendingLeaves()
        {
            return Ok();
        }

        [HttpPost]
        [Route("UpdateDoctorLeave")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateDoctorLeave(LeaveUpdateDTO leaveUpdateDTO)
        {
            return Ok(await _adminService.UpdateDoctorLeave(leaveUpdateDTO));
        }
    }
}
