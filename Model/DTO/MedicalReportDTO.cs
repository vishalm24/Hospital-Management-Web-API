namespace Hospital_Management.Model.DTO
{
    public class MedicalReportDTO
    {
        public int Id { get; set; }
        public string PatientName { get; set; }
        public string DoctorName { get; set; }
        public string DepartmentName { get; set; }
        public string Symptoms { get; set; }
        public string Diagnose { get; set; }
        public string Treatments { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime PastContactedDate { get; set; }
    }
}
