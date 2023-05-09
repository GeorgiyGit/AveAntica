using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Server.Api.Middlewares
{
    public class JwtTokenMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public JwtTokenMiddleware(RequestDelegate next, ILogger<JwtTokenMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Headers.ContainsKey("Authorization"))
            {
                string authorizationHeader = context.Request.Headers["Authorization"];

                if (authorizationHeader.StartsWith("Bearer "))
                {
                    // JWT token exists in the Authorization header
                    _logger.LogInformation(authorizationHeader);
                    _logger.LogInformation("Request has a JWT token.");
                }
            }
            else
            {
                // JWT token does not exist in the Authorization header
                _logger.LogInformation("Request does not have a JWT token.");
            }

            // Call the next middleware in the pipeline
            await _next(context);
        }
    }
}