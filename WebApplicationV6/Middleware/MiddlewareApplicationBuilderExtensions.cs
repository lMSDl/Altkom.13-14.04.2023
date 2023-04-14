namespace WebApplicationV6.Middleware
{
    public static class MiddlewareApplicationBuilderExtensions
    {
        public static IApplicationBuilder Use2(this IApplicationBuilder app)
        {
            return app.UseMiddleware<Use2Middleware>();
        }
        public static IApplicationBuilder Use1(this IApplicationBuilder app)
        {
            return app.UseMiddleware<Use1Middleware>();
        }
        public static IApplicationBuilder HelloRun(this IApplicationBuilder app)
        {
            return app.UseMiddleware<RunMiddleware>();
        }
        public static IApplicationBuilder TimeRun(this IApplicationBuilder app)
        {
            return app.UseMiddleware<TimeRunMiddleware>();
        }
    }
}
