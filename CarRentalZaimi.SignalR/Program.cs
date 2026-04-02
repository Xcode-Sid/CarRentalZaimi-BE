using CarRentalZaimi.SignalR.Extentions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalRServices();
//builder.Services.AddApplicationCommon();

var app = builder.Build();

app.MapSignalRHubs();

app.Run();