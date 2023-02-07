using Entity.Dtos;
using Entity.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class AuthorizationMiddlewareHandlerService : IAuthorizationMiddlewareResultHandler
    {
        private readonly AuthorizationMiddlewareResultHandler DefaultHandler = new AuthorizationMiddlewareResultHandler();
        public async Task HandleAsync(RequestDelegate next, HttpContext context, AuthorizationPolicy policy, PolicyAuthorizationResult authorizeResult)
        {
            if (authorizeResult.Challenged)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                //response body
                await context.Response.WriteAsJsonAsync(new ErrorResponseModel(ResponseCode.UnAuthorize, "UnAuthorized: Access is Denied due invalid credential."));
                return;
            }
            if (authorizeResult.Forbidden)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                //response body
                await context.Response.WriteAsJsonAsync(new ErrorResponseModel(ResponseCode.Forbidden, "Permission: You do not have permission to access this resource"));
                return;
            }
            await DefaultHandler.HandleAsync(next, context, policy, authorizeResult);
        }
    }
}
