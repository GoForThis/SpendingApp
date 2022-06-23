using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using SpendingApp;
using SpendingApp.Middleware;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using SpendingApp.Authorization;
using SpendingApp.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>
    (options => options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString")));

builder.Services.AddScoped<Seeder>();

builder.Services.AddScoped<ErrorHandlingMiddleware>();

// Authorization
var authorizationSettings = new AuthorizationSettings();
builder.Configuration.GetSection("Authentication").Bind(authorizationSettings);
builder.Services.AddSingleton(authorizationSettings);
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = "Bearer";
    option.DefaultScheme = "Bearer";
    option.DefaultChallengeScheme = "Bearer";
}).AddJwtBearer(option =>
{
    option.RequireHttpsMetadata = false;
    option.SaveToken = true;
    option.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = authorizationSettings.JwtIssuer,
        ValidAudience = authorizationSettings.JwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authorizationSettings.JwtKey)),
    };
});

builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

var app = builder.Build();
var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<Seeder>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
seeder.Seed();

app.UseHttpsRedirection();
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();

app.Run();
