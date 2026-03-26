using CarRentalZaimi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<ApplicationDbContext>(opt =>
{
    opt.UseMySQL(builder.Configuration.GetValue<string>("Default")!,
        opt => opt.MigrationsAssembly(typeof(Program).Assembly.FullName));
});
var app = builder.Build();
app.Run();

