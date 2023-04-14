//public class Program {

//    public void Main(string[] args)
//    {

        var variable = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        Console.WriteLine(variable);


        var builder = WebApplication.CreateBuilder(args);


        var app = builder.Build();

        app.Use(async (context, next) =>
        {
            Console.WriteLine("Begin use1");
            await next();
            Console.WriteLine("End use1");
        });

        app.Map("/time", mapApp =>
        {
            mapApp.Use(async (context, next) =>
            {
                Console.WriteLine("Begin mapuse1");
                await next();
                Console.WriteLine("End mapuse1");
            });

            mapApp.Run(async context =>
            {
                await context.Response.WriteAsync(DateTime.Now.ToLongTimeString());
            });
        });

        app.MapWhen(context => context.Request.Query.TryGetValue("name", out _), mapWhenApp =>
        {
            mapWhenApp.Run(async context =>
            {
                await context.Response.WriteAsync($"Hello {context.Request.Query["name"]}!");
            });
        });

        app.Use(async (context, next) =>
        {
            Console.WriteLine("Begin use2");
            await next();
            Console.WriteLine("End use2");
        });

        app.Run(async context =>
        {
            await context.Response.WriteAsync("Hello world!");
        });

app.Run();

//    }
//    }