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
        [Route("GetLeavesByStatus")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetLeavesByStatus(string status)
        {
            return Ok(await _adminService.GetLeavesByStatus(status));
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
