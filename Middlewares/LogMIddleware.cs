using Hospital_Management.Services.IServices;
using System.Diagnostics;

namespace Hospital_Management.Middlewares
{
    public class LogMIddleware
    {
        private readonly RequestDelegate _next;
        public LogMIddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context, ICustomLogger customLogger)
        {
            var cid = context.Request.Headers.ContainsKey("X-Correlation-ID") 
                ? context.Request.Headers["X-Correlation-ID"].ToString() 
                : Guid.NewGuid().ToString();
            context.Items["CID"] = cid;
            context.Response.Headers["X-Correlation-ID"] = cid;
            var stopwatch = Stopwatch.StartNew();
            try
            {
                await _next(context);
                stopwatch.Stop();
                await customLogger.LogAsync(cid, "Successfully processed request", TimeOnly.FromTimeSpan(stopwatch.Elapsed));
            }
            catch
            {
                throw;
            }
        }
    }
}
