//public class Program {

//    public void Main(string[] args)
//    {

using WebApplicationV6.Middleware;

var variable = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
Console.WriteLine(variable);


var builder = WebApplication.CreateBuilder(args);

//stosuj¹c IMiddleware wymagana jest rejestracja w serwisach
builder.Services.AddSingleton<Use2Middleware>();
builder.Services.AddSingleton<RunMiddleware>();


var app = builder.Build();

//app.UseMiddleware<Use1Middleware>();
app.Use1();

app.Map("/time", TimeApp());

app.MapWhen(context => context.Request.Query.TryGetValue("name", out _), WhenNameApp());

//app.UseMiddleware<Use2Middleware>();
//MiddlewareApplicationBuilderExtensions.Use2(app);
app.Use2();

//app.UseMiddleware<RunMiddleware>();
app.HelloRun();

app.Run();



static Action<IApplicationBuilder> TimeApp()
{
    return mapApp =>
    {
        mapApp.Use(async (context, next) =>
        {
            Console.WriteLine("Begin mapuse1");
            await next();
            Console.WriteLine("End mapuse1");
        });

        mapApp.UseMiddleware<TimeRunMiddleware>();
    };
}

static Action<IApplicationBuilder> WhenNameApp()
{
    return mapWhenApp =>
    {
        mapWhenApp.Run(async context =>
        {
            await context.Response.WriteAsync($"Hello {context.Request.Query["name"]}!");
        });
    };
}

//    }
//    }