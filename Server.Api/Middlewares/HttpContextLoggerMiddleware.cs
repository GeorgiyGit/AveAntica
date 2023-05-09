using System.Text;

namespace Server.Api.Middlewares
{
    public class HttpContextLoggerMiddleware
    {
        private readonly RequestDelegate _next;

        public HttpContextLoggerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Log the HTTP context
            Console.WriteLine($"HTTP Context:\n{GetHttpContextDetails(context)}");

            // Call the next middleware in the pipeline
            await _next(context);
        }

        private string GetHttpContextDetails(HttpContext context)
        {
            var request = context.Request;
            var response = context.Response;
            var user = context.User;

            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"Request Method: {request.Method}");
            stringBuilder.AppendLine($"Request Scheme: {request.Scheme}");
            stringBuilder.AppendLine($"Request Host: {request.Host}");
            stringBuilder.AppendLine($"Request Path: {request.Path}");
            stringBuilder.AppendLine($"Request Query String: {request.QueryString}");
            stringBuilder.AppendLine($"Request Protocol: {request.Protocol}");

            stringBuilder.AppendLine($"Response Status Code: {response.StatusCode}");

            if (user.Identity.IsAuthenticated)
            {
                stringBuilder.AppendLine($"User Identity Name: {user.Identity.Name}");
            }
            else
            {
                stringBuilder.AppendLine($"User Identity: not authenticated");
            }

            return stringBuilder.ToString();
        }
    }
}
