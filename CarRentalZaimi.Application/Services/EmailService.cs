using CarRentalZaimi.Application.Common.Email;
using CarRentalZaimi.Application.Common.Errors;
using CarRentalZaimi.Application.DTOs.ApiResponse;
using CarRentalZaimi.Application.Interfaces.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using CarRentalZaimi.Logging;

namespace CarRentalZaimi.Application.Services;

public class EmailService(IOptions<EmailSettings> emailSettingsOptions,
    ILogger<EmailService> logger,IErrorService errorService) : IEmailService
{

    private readonly EmailSettings _emailSettings = emailSettingsOptions.Value;
    private readonly ILogger<EmailService> _logger = logger;

    public async Task<ApiResponse<bool>> SendForgotPasswordEmailAsync(string email, string firstName, string resetLink, CancellationToken cancellationToken = default)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var resourceName = assembly.GetManifestResourceNames()
            .FirstOrDefault(n => n.EndsWith("CarRentalZaimiForgotPasswordEmailTemplate_en.html"));

        if (resourceName == null)
        {
            _logger.Error("Forgot password email template not found in embedded resources.");
            return errorService.CreateFailure<bool>(ErrorCodes.EXTERNAL_SERVICE_ERROR);
        }

        using var stream = assembly.GetManifestResourceStream(resourceName)!;
        using var reader = new StreamReader(stream);
        var body = await reader.ReadToEndAsync(cancellationToken);

        body = body
            .Replace("{{FirstName}}", firstName)
            .Replace("{{ResetLink}}", resetLink)
            .Replace("{{Year}}", DateTime.UtcNow.Year.ToString());

        return await SendEmailAsync(email, "Reset Your Password", body, isHtml: true, cancellationToken);
    }

    public async Task<ApiResponse<bool>> SendEmailAsync(string to, string subject, string body, bool isHtml = true, CancellationToken cancellationToken = default)
    {
        try
        {
            using var client = new SmtpClient(_emailSettings.SmtpServer, _emailSettings.SmtpPort)
            {
                EnableSsl = _emailSettings.EnableSsl,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_emailSettings.Username, _emailSettings.Password)
            };

            using var message = new MailMessage
            {
                From = new MailAddress(_emailSettings.FromEmail!, _emailSettings.FromName),
                Subject = subject,
                Body = body,
                IsBodyHtml = isHtml
            };

            message.To.Add(to);
            await client.SendMailAsync(message, cancellationToken);

            return ApiResponse<bool>.SuccessResponse(true);
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Failed to send email to {Email}", to);
            return ApiResponse<bool>.FailureResponse($"Failed to send email: {ex.Message}");
        }
    }

}

