using System.Net;
using System.Text.Json;
using System;
using Azure;
using Hospital_Management.Model;

namespace Hospital_Management.Controllers
{
    public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        public GlobalExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception ex)
            {
                await HandlerExceptionAsync(context, ex);
            }
        }
        public async Task HandlerExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            var response = context.Response;
            ResponseModel<string> exModel = new ResponseModel<string>();
            switch (ex)
            {
                case ApplicationException:
                    exModel.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    exModel.Message = ex.Message;
                    break;
                case FileNotFoundException:
                    exModel.StatusCode = (int)HttpStatusCode.NotFound;
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    exModel.Message = ex.Message;
                    break;
                default:
                    exModel.StatusCode = (int)HttpStatusCode.InternalServerError;
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    exModel.Message = "Internal server error. Please try later.";
                    break;
            }
            var result = JsonSerializer.Serialize(exModel);
            await context.Response.WriteAsync(result); 
        }
    }
}
