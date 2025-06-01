using System.Net;
using System.Text.Json;
using System;
using Azure;
using Hospital_Management.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Hospital_Management.Exceptions;
using Hospital_Management.Services.IServices;

namespace Hospital_Management.Middlewares
{
    public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        public GlobalExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context, ICustomLogger customLogger)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandlerExceptionAsync(context, ex, customLogger);
            }
        }
        public async Task HandlerExceptionAsync(HttpContext context, Exception ex, ICustomLogger customLogger)
        {
            context.Response.ContentType = "application/json";
            var response = context.Response;
            ResponseModel<string> exModel = new ResponseModel<string>();
            string cid = context.Items["CID"]?.ToString()?? Guid.NewGuid().ToString();
            await customLogger.LogAsync(cid, ex.Message, TimeOnly.MinValue);
            switch (ex)
            {
                case BadRequestException:
                    exModel.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    exModel.Message = ex.Message;
                    break;
                case NotFoundException:
                    exModel.StatusCode = (int)HttpStatusCode.NotFound;
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    exModel.Message = ex.Message;
                    break;
                case ConflictException:
                    exModel.StatusCode = (int)HttpStatusCode.Conflict;
                    context.Response.StatusCode = (int)HttpStatusCode.Conflict;
                    exModel.Message = ex.Message;
                    break;
                case ForbiddenException:
                    exModel.StatusCode = (int)HttpStatusCode.Forbidden;
                    context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    exModel.Message = ex.Message;
                    break;
                case UnauthorizedException:
                    exModel.StatusCode = (int)HttpStatusCode.Unauthorized;
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    exModel.Message = ex.Message;
                    break;
                case ValidationException:
                    exModel.StatusCode = (int)HttpStatusCode.BadRequest;
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    exModel.Message = ex.Message;
                    break;
                default:
                    exModel.StatusCode = (int)HttpStatusCode.InternalServerError;
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    exModel.Message = ex.Message;
                    break;
            }
            var result = JsonSerializer.Serialize(exModel);
            await context.Response.WriteAsync(result);
        }
    }
}
