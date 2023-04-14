namespace WebApplicationV6.Middleware
{
    public class TimeRunMiddleware
    {
        public TimeRunMiddleware(RequestDelegate _)
        {
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await context.Response.WriteAsync(DateTime.Now.ToLongTimeString());
        }
    }
}
