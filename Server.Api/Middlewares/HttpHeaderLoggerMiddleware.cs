namespace Server.Api.Middlewares
{
    public class HttpHeaderLoggerMiddleware
    {
        private readonly RequestDelegate _next;

        public HttpHeaderLoggerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Log the value of the "User-Agent" header
            var userAgent = context.Request.Headers["User-Agent"].ToString();
            Console.WriteLine($"User-Agent: {userAgent}");

            // Call the next middleware in the pipeline
            await _next(context);
        }
    }
}
