using Hospital_Management.Data;
using Hospital_Management.Model;
using Hospital_Management.Services.IServices;
using System;
using System.Diagnostics;

namespace Hospital_Management.Services
{
    public class CustomLogger : ICustomLogger
    {
        private readonly ApplicationDbContext _context;

        public CustomLogger(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task LogAsync(string cid, string message, TimeOnly executionTime)
        {
            var entry = new LogEntry
            {
                CID = cid,
                Message = message,
                ExecutionTime = executionTime
            };

            _context.LogEntries.Add(entry);
            await _context.SaveChangesAsync();
        }
    }
}
