namespace WebApplicationV6.Middleware
{
    public class Use2Middleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            Console.WriteLine("Begin use2");
            await next(context);
            Console.WriteLine("End use2");
        }
    }
}
