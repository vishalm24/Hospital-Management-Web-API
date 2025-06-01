namespace Hospital_Management.Model
{
    public class LogEntry
    {
        public int Id { get; set; }
        public string CID { get; set; }
        public string Message { get; set; }
        public TimeOnly ExecutionTime { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
