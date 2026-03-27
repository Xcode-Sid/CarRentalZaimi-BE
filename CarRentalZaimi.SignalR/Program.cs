using CarRentalZaimi.SignalR.Extentions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalRServices();

var app = builder.Build();

app.MapSignalRHubs();

app.Run();