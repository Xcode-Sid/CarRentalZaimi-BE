using CarRentalZaimi.API.Handlers;
using CarRentalZaimi.Application.Common.Facebook;
using CarRentalZaimi.Application.Common.Google;
using CarRentalZaimi.Application.Common.Microsoft;
using CarRentalZaimi.Application.Common.Yahoo;
using CarRentalZaimi.Application.Extensions;
using CarRentalZaimi.Application.Interfaces.Services;
using CarRentalZaimi.Application.Services;
using CarRentalZaimi.Infrastructure;
using CarRentalZaimi.Infrastructure.Seed;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.Extensions.Options;
using Scalar.AspNetCore;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((ctx, cfg) => cfg
        .ReadFrom.Configuration(ctx.Configuration)
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .WriteTo.MySQL(
            ctx.Configuration.GetConnectionString("Default")!,
            "AppLogs"));


    builder.Services
        .AddAuthentication(options =>
        {
            options.DefaultScheme = "GoogleAuth";
            options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
        })
        .AddCookie("GoogleAuth", options =>
        {
            options.Cookie.SameSite = SameSiteMode.Lax;
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        })
        .AddGoogle(options =>
        {
            options.SignInScheme = "GoogleAuth";
            options.ClientId = builder.Configuration.GetValue<string>("Authentication:Google:ClientId")!;
            options.ClientSecret = builder.Configuration.GetValue<string>("Authentication:Google:ClientSecret")!;
            options.CallbackPath = "/api/v1/Auth/google-response";
            options.SaveTokens = true;
        });


    builder.Services.AddSingleton(new GoogleOAuthSettings
    {
        ClientId = builder.Configuration["Authentication:Google:ClientId"]!,
        ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"]!,
        TokenUrl = builder.Configuration["Authentication:Google:TokenUrl"]!,
        UserInfoUrl = builder.Configuration["Authentication:Google:UserInfoUrl"]!
    });
    builder.Services.AddHttpClient<IGoogleOAuthService, GoogleOAuthService>();

    builder.Services.AddSingleton(new FacebookOAuthSettings
    {
        AppId = builder.Configuration["Authentication:Facebook:AppId"]!,
        AppSecret = builder.Configuration["Authentication:Facebook:AppSecret"]!,
        TokenUrl = builder.Configuration["Authentication:Facebook:TokenUrl"]!,
        UserInfoUrl = builder.Configuration["Authentication:Facebook:UserInfoUrl"]!,
        ApiVersion = builder.Configuration["Authentication:Facebook:ApiVersion"]!
    });
    builder.Services.AddHttpClient<IFacebookOAuthService, FacebookOAuthService>();

    builder.Services.AddSingleton(new MicrosoftOAuthSettings
    {
        ClientId = builder.Configuration["Authentication:Microsoft:ClientId"]!,
        TenantId = builder.Configuration["Authentication:Microsoft:TenantId"]!,
        TokenUrl = builder.Configuration["Authentication:Microsoft:TokenUrl"]!,
        UserInfoUrl = builder.Configuration["Authentication:Microsoft:UserInfoUrl"]!
    });
    builder.Services.AddHttpClient<IMicrosoftOAuthService, MicrosoftOAuthService>();

    builder.Services.AddSingleton(new YahooOAuthSettings
    {
        ClientId = builder.Configuration["Authentication:Yahoo:ClientId"]!,
        TokenUrl = builder.Configuration["Authentication:Yahoo:TokenUrl"]!,
        UserInfoUrl = builder.Configuration["Authentication:Yahoo:UserInfoUrl"]!
    });
    builder.Services.AddHttpClient<IYahooOAuthService, YahooOAuthService>();



    // ================== CORS ==================
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAll", policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  .WithExposedHeaders("X-Pagination", "X-Request-Id");
        });
    });


    builder.Services.AddControllers();
    builder.Services.AddOpenApi();
    builder.Services.AddProblemDetails();
    builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
    builder.Services.AddApplicationServices();
    builder.Services.AddInfrastructureServices(builder.Configuration);
    builder.Services.AddHttpContextAccessor();

    var app = builder.Build();

    using (var scope = app.Services.CreateScope())
    {
        await DatabaseSeeder.SeedAllAsync(scope.ServiceProvider);
    }

    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
        app.MapScalarApiReference();
    }

    app.UseStaticFiles();
    app.UseSerilogRequestLogging();
    app.UseExceptionHandler();
    app.UseCors("AllowAll");
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
