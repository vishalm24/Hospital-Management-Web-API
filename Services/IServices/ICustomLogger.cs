namespace Hospital_Management.Services.IServices
{
    public interface ICustomLogger
    {
        Task LogAsync(string cid, string message, TimeOnly executionTime);
    }
}
