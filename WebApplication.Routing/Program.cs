var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Use(async (context, next) =>
{
    Console.WriteLine(context.GetEndpoint()?.DisplayName ?? "NULL");
    await next();
});

app.UseRouting();

app.Use(async (context, next) =>
{
    Console.WriteLine(context.GetEndpoint()?.DisplayName ?? "NULL");
    await next();
});

/*
app.Map("/Bye", mapApp =>
{
    *//*mapApp.UseRouting();
    mapApp.Use(async (context, next) =>
    {
        Console.WriteLine(context.GetEndpoint()?.DisplayName ?? "NULL");
        await next();
    });

    mapApp.UseEndpoints(endpoints =>
    {
        endpoints.MapGet("/Bye", () => "Endpoint Bye!");
    });*//*

    mapApp.Run(async context =>
    {
        await context.Response.WriteAsync("Bye!");
    });
});*/


//app.UseEndpoints( ...
app.MapGet("/Hello", () => "Hello!");
app.MapGet("/Bye/Bye", () => "Bye!");
app.MapGet("/Bye/{name:minlength(10)}", (string name) => $"Bye {name}");


app.Run();
