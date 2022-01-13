using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SINPRO.Middleware
{
    //public class AuthenticationMiddleware
    //{
    //    public static HttpContext GetHttpContext;
    //    public static string accessToken { get; set; }
    //    public AuthenticationMiddleware(RequestDelegate next)
    //    {
    //        _next = next;
    //    }
    //    private readonly RequestDelegate _next;
    //    public async Task Invoke(HttpContext context)
    //    {
    //        GetHttpContext = context;
    //        string authHeader = context.Request.Headers["Authorization"];
    //        if (authHeader != null)
    //            accessToken = authHeader.Replace("Bearer ", "").Replace("bearer ", "");
    //        await _next(context);
    //    }
    //}
}
