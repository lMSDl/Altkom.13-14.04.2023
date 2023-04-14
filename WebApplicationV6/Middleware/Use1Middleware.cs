namespace WebApplicationV6.Middleware
{
    public class Use1Middleware
    {

        private readonly RequestDelegate requestDelegate;

        public Use1Middleware(RequestDelegate requestDelegate)
        {
            this.requestDelegate = requestDelegate;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine("Begin use1");
            await requestDelegate(context);
            Console.WriteLine("End use1");
        }
    }
}
