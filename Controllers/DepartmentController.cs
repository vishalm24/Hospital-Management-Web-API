using Hospital_Management.Model.DTO;
using Hospital_Management.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;
        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }
        [HttpPost]
        [Route("AddDepartment")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddDepartment([FromBody] DepartmentAddDTO departmentAddDTO)
        {
            return Ok(await _departmentService.AddDepartment(departmentAddDTO));
        }
        [HttpGet]
        [Route("GetAllDepartments")]
        [Authorize(Roles = "Admin,Receptionist,Doctor")]
        public async Task<IActionResult> GetAllDepartments()
        {
            return Ok(await _departmentService.GetAllDepartments());
        }
        [HttpGet]
        [Route("GetDepartmentById/{id}")]
        [Authorize(Roles = "Admin,Receptionist,Doctor")]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            return Ok(await _departmentService.GetDepartmentById(id));
        }
        [HttpPut]
        [Route("UpdateDepartment")]
        [Authorize("UpdateDepartment")]
        public async Task<IActionResult> UpdateDepartment([FromBody] DepartmentDTO departmentUpdateDTO)
        {
            return Ok(await _departmentService.UpdateDepartment(departmentUpdateDTO));
        }
        [HttpDelete]
        [Route("RemoveDepartment/{id}")]
        [Authorize(Roles = "Admin,Receptionist,Doctor")]
        public async Task<IActionResult> RemoveDepartment(int id)
        {
            return Ok(await _departmentService.DeleteDepartment(id));
        }
    }
}
