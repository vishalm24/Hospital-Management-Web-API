namespace Hospital_Management.Model
{
    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }
        public string ActiveTreatment { get; set; }
        public bool IsActive { get; set; }
    }
}
