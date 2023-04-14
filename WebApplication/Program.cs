using Models;
using Services.Bogus;
using Services.Bogus.Fakers;
using Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddSingleton<IEntityService<User>, EntityService<User>>();
builder.Services.AddSingleton<IEntityService<User>>(services => new EntityService<User>(services.GetService<EntityFaker<User>>()!, services.GetService<IConfiguration>().GetValue<int>("FakerCount")));
builder.Services.AddTransient<EntityFaker<User>, UserFaker>();


var app = builder.Build();

app.UseHttpsRedirection();
app.UseHsts();


app.MapGet("/users", async context => await context.Response.WriteAsJsonAsync(await context.RequestServices.GetService<IEntityService<User>>()!.ReadAsync()) );
app.MapGet("/users/{id:int}", async (HttpContext context, int id) => await context.Response.WriteAsJsonAsync(await context.RequestServices.GetService<IEntityService<User>>()!.ReadAsync(id)));
app.MapPost("/users", async (HttpContext context, User user) => await context.RequestServices.GetService<IEntityService<User>>()!.CreateAsync(user));
app.MapDelete("/users/{id:int}", async (HttpContext context, int id) => await context.RequestServices.GetService<IEntityService<User>>()!.DeleteAsync(id));

app.Run();
