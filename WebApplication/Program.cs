using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Models;
using Services.Bogus;
using Services.Bogus.Fakers;
using Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

byte[] securityKey = Guid.NewGuid().ToByteArray();

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddSingleton<IEntityService<User>, EntityService<User>>();
builder.Services.AddSingleton<IEntityService<User>>(services => new EntityService<User>(services.GetService<EntityFaker<User>>()!, services.GetService<IConfiguration>().GetValue<int>("FakerCount")));
builder.Services.AddTransient<EntityFaker<User>, UserFaker>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromSeconds(30);
        options.LoginPath = "/login";
    });


/*builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(securityKey),
        RequireExpirationTime = true
    };
});*/

builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("AdminOnly", policy => policy.RequireUserName("admin"));
    });



var app = builder.Build();

app.UseHttpsRedirection();
app.UseHsts();

app.UseAuthentication();
app.UseAuthorization();


app.MapGet("/users", [Authorize] async (context) => await context.Response.WriteAsJsonAsync(await context.RequestServices.GetService<IEntityService<User>>()!.ReadAsync()) );
app.MapGet("/users/{id:int}", [Authorize(Policy = "AdminOnly")] async (HttpContext context, int id) => await context.Response.WriteAsJsonAsync(await context.RequestServices.GetService<IEntityService<User>>()!.ReadAsync(id)));
app.MapPost("/users", [Authorize(Roles = "CREATE")] async (HttpContext context, User user) => await context.RequestServices.GetService<IEntityService<User>>()!.CreateAsync(user));
app.MapDelete("/users/{id:int}", [Authorize(Roles = "DELETE, USER")] async (HttpContext context, int id) => await context.RequestServices.GetService<IEntityService<User>>()!.DeleteAsync(id));

//[Authorize(Roles = "DELETE, USER")] - wymagane jedno z wymienionych uprawnieñ
//[Authorize(Roles = "DELETE")][Authorize(Roles = "USER")] - wymagane dwa uprawnienia

/*app.MapPost("/login", async (HttpContext context, User user) =>
{
    if ((user.Login != "admin" || user.Password != "nimda") && (user.Login != "user" || user.Password != "resu"))
    {
        return;
    }

    var tokenHandler = new JwtSecurityTokenHandler();
    var tokenDescriptor = new SecurityTokenDescriptor();

    var claims = new List<Claim>();
    claims.Add(new Claim(ClaimTypes.Name, user.Login));
    claims.Add(new Claim(ClaimTypes.Role, "USER"));
    claims.Add(new Claim(ClaimTypes.DateOfBirth, user.BithDate.ToShortDateString()));
    if (user.Login == "admin")
    {
        claims.Add(new Claim(ClaimTypes.Role, "CREATE"));
        claims.Add(new Claim(ClaimTypes.Role, "DELETE"));
    }

    tokenDescriptor.Subject = new System.Security.Claims.ClaimsIdentity(claims);
    tokenDescriptor.Expires = DateTime.Now.AddSeconds(30);
    tokenDescriptor.SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(securityKey), SecurityAlgorithms.HmacSha256);

    var token = tokenHandler.CreateToken(tokenDescriptor);

    await context.Response.WriteAsync(tokenHandler.WriteToken(token));
});*/

app.MapGet("/login", async context =>
{
    var claims = new List<Claim>();

    claims.Add(new Claim(ClaimTypes.Name, "admin"));
    var identity = new System.Security.Claims.ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
    var principal = new ClaimsPrincipal(identity);

    await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

});

app.Run();
