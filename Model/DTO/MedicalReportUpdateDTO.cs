namespace Hospital_Management.Model.DTO
{
    public class MedicalReportUpdateDTO
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public int DepartmentId { get; set; }
        public string Symptoms { get; set; }
        public string Diagnose { get; set; }
        public string Treatments { get; set; }
    }
}
