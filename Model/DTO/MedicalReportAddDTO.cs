namespace Hospital_Management.Model.DTO
{
    public class MedicalReportAddDTO
    {
        public int PatientId { get; set; }
        public int AppointmentId { get; set; }
        public int DoctorId { get; set; }
        public int DepartmentId { get; set; }
        public string Symptoms { get; set; }
        public string Diagnose { get; set; }
        public string Treatments { get; set; }
        public DateTime PastContactedDate { get; set; }
    }
}
