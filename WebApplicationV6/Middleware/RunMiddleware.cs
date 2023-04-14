namespace WebApplicationV6.Middleware
{
    public class RunMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate _)
        {
            await context.Response.WriteAsync("Hello world!");
        }
    }
}
