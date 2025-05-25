using Hospital_Management.Model;
using Hospital_Management.Model.DTO;

namespace Hospital_Management.Services.IServices
{
    public interface IDepartmentService
    {
        public Task<ResponseModel<List<DepartmentDTO>>> GetAllDepartments();
        public Task<ResponseModel<DepartmentDTO>> AddDepartment(DepartmentAddDTO departmentAddDTO);
        public Task<ResponseModel<string>> UpdateDepartment(DepartmentDTO departmentUpdateDTO);
        public Task<ResponseModel<string>> DeleteDepartment(int id);
    }
}
