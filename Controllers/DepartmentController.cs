using Hospital_Management.Model.DTO;
using Hospital_Management.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

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
        [Route("AllDepartments")]
        [Authorize(Roles = "Admin,Receptionist")]
        public async Task<IActionResult> GetAllDepartments()
        {
            return Ok(await _departmentService.GetAllDepartments());
        }
        [HttpPut]
        [Route("EditDepartment")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateDepartment([FromBody] DepartmentDTO departmentUpdateDTO)
        {
            return Ok(await _departmentService.UpdateDepartment(departmentUpdateDTO));
        }
        [HttpDelete]
        [Route("RemoveDepartment")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            return Ok(await _departmentService.DeleteDepartment(id));
        }
    }
}
