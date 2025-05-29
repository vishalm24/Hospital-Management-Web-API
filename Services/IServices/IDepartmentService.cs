using Hospital_Management.Model;
using Hospital_Management.Model.DTO;

namespace Hospital_Management.Services.IServices
{
    public interface IDepartmentService
    {
        Task<ResponseModel<List<DepartmentDTO>>> GetAllDepartments();
        Task<ResponseModel<DepartmentDTO>> GetDepartmentById(int id);
        Task<ResponseModel<DepartmentDTO>> AddDepartment(DepartmentAddDTO departmentAddDTO);
        Task<ResponseModel<string>> UpdateDepartment(DepartmentDTO departmentUpdateDTO);
        Task<ResponseModel<string>> DeleteDepartment(int id);
    }
}
