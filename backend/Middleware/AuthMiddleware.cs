﻿using cms_api.Data;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Text;
using cms_api.Utils;

namespace cms_api.Middleware
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;

        // list of routes only accessible for the hyper users
        private readonly List<string> HyperRoutes = new List<string>() { "user", "themes", "theme" };


        public AuthMiddleware(RequestDelegate next)
        {
            this._next = next;
        }


        public async Task Invoke(HttpContext context)
        {
            try
            {
                var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                if (token == null)
                {
                    // return the response with unauthorized code
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    context.Response.ContentType = "text/plain";
                    await context.Response.WriteAsync("UnAuthorized For This Page");
                    return;
                }
                else
                {
                    // get the last path of the route
                    string[] paths = context.Request.Path.Value!.Split("/");
                    string lastPath = paths.Last();
                    if(Guid.TryParse(lastPath, out var id))
                    {
                        lastPath = paths[paths.Length - 2];
                    }
                    List<string> data = TokenHelper.DecodeToken(token);

                    // check if the path contains any hyper routes
                    if (HyperRoutes.Contains(lastPath))
                    {
                        Console.WriteLine(data[1]);
                        // verify if the user is eligible to access
                        bool isAuthorized = data[1] == "hyper";
                        if (!isAuthorized)
                        {
                            // return the response with unauthorized code
                            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                            context.Response.ContentType = "text/plain";
                            await context.Response.WriteAsync("UnAuthorized For This Page");
                            return;
                        }
                    }

                    context.Items["user"] = data[0];

                    await _next(context);
                }

            }
            catch (Exception ex)
            {
             
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync(ex.Message);
                return;

            }

        }
    }


}

