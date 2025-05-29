using Hospital_Management.Data;
using Hospital_Management.Exceptions;
using Hospital_Management.Model;
using Hospital_Management.Model.DTO;
using Hospital_Management.Services.IServices;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Management.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly ApplicationDbContext _db;
        public DepartmentService(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<ResponseModel<DepartmentDTO>> AddDepartment(DepartmentAddDTO departmentAddDTO)
        {
            var result = new ResponseModel<DepartmentDTO>();
            var department = await _db.Departments.Where(d => d.Name == departmentAddDTO.Name).FirstOrDefaultAsync();
            if (department != null && department.IsActive == false)
            {
                department.IsActive = true;
                _db.Departments.Update(department);
                result.SetSeccess(ShowDetails(department));
            }
            else if (department != null)
            {
                result.SetConflict(ShowDetails(department));
                throw new ConflictException("Department already exists.");
            }
            else
            {
                department = new Department
                {
                    Name = departmentAddDTO.Name,
                    IsActive = true
                };
                await _db.Departments.AddAsync(department);
            }
            await _db.SaveChangesAsync();
            result.SetSeccess(ShowDetails(department));
            return result;
        }

        //public async Task<ResponseModel<DepartmentDTO>> AddDepartment(DepartmentAddDTO departmentAddDTO)
        //{
        //    var result = new ResponseModel<DepartmentDTO>();
        //    try
        //    {
        //        var isExist = await _db.Departments.AnyAsync(d => d.Name == departmentAddDTO.Name);
        //        if (isExist)
        //            throw new ConflictException("Department already exists.");
        //        var department = new Department
        //        {
        //            Name = departmentAddDTO.Name,
        //            IsActive = true
        //        };
        //        await _db.Departments.AddAsync(department);
        //        await _db.SaveChangesAsync();
        //        var departmentDTO = new DepartmentDTO
        //        {
        //            Id = department.Id,
        //            Name = department.Name
        //        };
        //        result.SetSeccess(departmentDTO);

        //    }
        //    catch (Exception ex)
        //    {
        //        result.SetFailure(ex.Message);
        //    }
        //    return result;
        //}

        public async Task<ResponseModel<string>> DeleteDepartment(int id)
        {
            var result = new ResponseModel<string>();
            var department = await _db.Departments.FirstOrDefaultAsync(d => d.Id == id && d.IsActive == true);
            if (department == null)
                throw new NotFoundException($"Department consisting id {id} does not exist.");
            department.IsActive = false;
            _db.Departments.Update(department);
            await _db.SaveChangesAsync();
            result.SetSeccess($"Department {department.Name} is deleted successfully.");
            return result;
        }

        public async Task<ResponseModel<List<DepartmentDTO>>> GetAllDepartments()
        {
            var result = new ResponseModel<List<DepartmentDTO>>();
            var departments = await _db.Departments.Where(d => d.IsActive == true).ToListAsync();
            if (departments == null)
                throw new NotFoundException("Department does not exist.");
            var data = new List<DepartmentDTO>();
            foreach (var item in departments)
            {
                data.Add(ShowDetails(item));
            }
            result.SetSeccess(data);
            return result;
        }

        public async Task<ResponseModel<DepartmentDTO>> GetDepartmentById(int id)
        {
            var result = new ResponseModel<DepartmentDTO>();
            var department = await _db.Departments.FirstOrDefaultAsync(d => d.Id == id && d.IsActive == true);
            if(department == null)
                throw new NotFoundException($"Department consisting id {id} does not exist.");
            result.SetSeccess(ShowDetails(department));
            return result;
        } 

        public async Task<ResponseModel<string>> UpdateDepartment(DepartmentDTO departmentUpdateDTO)
        {
            var result = new ResponseModel<string>();
            var department = await _db.Departments.FirstOrDefaultAsync(d => d.Id == departmentUpdateDTO.Id && d.IsActive == true);
            if(department == null)
                throw new NotFoundException($"Department consisting id {departmentUpdateDTO.Id} does not exist.");
            var isExist = await _db.Departments.AnyAsync(d => d.Name == departmentUpdateDTO.Name);
            if (isExist)
                throw new ConflictException("Department already exists.");
            if (!string.IsNullOrEmpty(departmentUpdateDTO.Name))
                department.Name = departmentUpdateDTO.Name;
            _db.Departments.Update(department);
            await _db.SaveChangesAsync();
            result.SetSeccess($"Department consiting Id = {departmentUpdateDTO.Id} is updated");
            return result;
        }

        public DepartmentDTO ShowDetails(Department department)
        {
            return new DepartmentDTO
            {
                Id = department.Id,
                Name = department.Name
            };
        }
    }
}
