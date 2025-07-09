using Hospital_Management.Data;
using Hospital_Management.Exceptions;
using Hospital_Management.Model;
using Hospital_Management.Model.DTO;
using Hospital_Management.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Management.Services
{
    public class AdminService: IAdminService
    {
        private readonly ApplicationDbContext _db;
        public AdminService(ApplicationDbContext db)
        {
            _db = db;
        }
        //public async Task<ResponseModel<LeaveGetDTO>> GetPendingLeaves()
        //{
        //    var result = new ResponseModel<LeaveGetDTO>();

        //    return result;
        //}
        public async Task<ResponseModel<List<LeaveGetDTO>>> GetLeavesByStatus(string status)
        {
            if (string.IsNullOrEmpty(status))
                status = "Pending";
            var result = new ResponseModel<List<LeaveGetDTO>>();
            var leaves = await _db.Set<LeaveGetDTO>().FromSqlInterpolated($"Execute GetLeavesByStatus @Status={status}").ToListAsync();
            if(leaves == null)
                throw new NotFoundException($"No leaves found with the {status} status.");
            result.SetSeccess(leaves);
            return result;
        }
        public async Task<ResponseModel<string>> UpdateDoctorLeave(LeaveUpdateDTO leaveUpdateDTO)
        {
            var result = new ResponseModel<string>();
            var leave = await _db.Leaves.FindAsync(leaveUpdateDTO.Id);
            if (leave == null)
            {
                throw new NotFoundException("Leave not found.");
            }
            leave.Status = leaveUpdateDTO.Status;
            _db.Leaves.Update(leave);
            await _db.SaveChangesAsync();
            result.SetSeccess($"Leave status updated to {leaveUpdateDTO.Status} successfully.");
            return result;
        }
    }
}
