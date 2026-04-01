using CarRentalZaimi.Application.Interfaces.Services;
using CarRentalZaimi.Infrastructure.Persistence;
using CarRentalZaimi.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUserContext, UserContext>();

builder.Services.AddDbContext<ApplicationDbContext>(opt =>
{
    opt.UseMySQL(builder.Configuration.GetConnectionString("Default")!,
        opt => opt.MigrationsAssembly(typeof(Program).Assembly.FullName));
});
var app = builder.Build();
app.Run();

