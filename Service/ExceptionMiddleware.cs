using Entity;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {

            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
            
        }
        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var code = HttpStatusCode.InternalServerError;

            if (exception is NotFoundException)
            {
                code = HttpStatusCode.NotFound;
            }
            else if (exception is BadRequestException)
            {
                code = HttpStatusCode.BadRequest;
            }
            else
            {
                code = HttpStatusCode.InternalServerError;
            }
            context.Response.StatusCode = (int)code;

            var error = new Error
            {
                StatusCode = context.Response.StatusCode,
                Message = exception.Message
            };

            var json = JsonConvert.SerializeObject(error);
            return context.Response.WriteAsync(json);
        }

    }
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        { }
    }

    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message)
        { }
    }
}
