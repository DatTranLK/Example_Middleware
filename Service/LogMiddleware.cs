using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class LogMiddleware
    {
        private readonly RequestDelegate _next;

        public LogMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var request = context.Request;
            var method = request.Method;
            var path = request.Path;
            var queryString = request.QueryString;

            Console.WriteLine($"[{DateTime.Now}] {method} request to {path}{queryString}");

            await _next.Invoke(context);

            var response = context.Response;
            var statusCode = response.StatusCode;
            
            Console.WriteLine($"[{DateTime.Now}] Response with {statusCode} status code");
        }
    }
}
