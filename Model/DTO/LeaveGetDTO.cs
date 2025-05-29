using Microsoft.AspNetCore.Http.HttpResults;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Hospital_Management.Model.DTO
{
    public class LeaveGetDTO
    {
        public int Id { get; set; }
        public string DoctorName { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; }
        public string AdminName { get; set; }
    }
}
