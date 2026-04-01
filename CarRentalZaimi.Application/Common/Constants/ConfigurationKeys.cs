namespace CarRentalZaimi.Application.Common.Constants;

public static class ConfigurationKeys
{
    public static class ConnectionStrings
    {
        public const string DefaultConnection = "DefaultConnection";
        public const string Redis = "Redis";
        public const string LoggingDatabase = "LoggingDatabase";
        public const string HangfireConnection = "HangfireConnection";
    }

    public static class Sections
    {
        public const string JwtSettings = "JwtSettings";
        public const string Kafka = "Kafka";
        public const string RabbitMQ = "RabbitMQ";
        public const string ApiKeys = "ApiKeys";
        public const string Telemetry = "Telemetry";
        public const string Email = "Email";
        public const string FileStorage = "FileStorage";
        public const string Stripe = "Stripe";
        public const string Sms = "Sms";
        public const string Firebase = "Firebase";
        public const string Weather = "Weather";
        public const string Loki = "Loki";

        /// <summary>Section path: "AI:Service"</summary>
        public const string AIService = "AI:Service";
        /// <summary>Section path: "AI:ChatGPT"</summary>
        public const string AIChatGPT = "AI:ChatGPT";
        /// <summary>Section path: "AI:Claude"</summary>
        public const string AIClaude = "AI:Claude";
    }

    public static class JwtSettingKeys
    {
        public const string SecretKey = "SecretKey";
        public const string Issuer = "Issuer";
        public const string Audience = "Audience";
        public const string ExpiryMinutes = "ExpiryMinutes";
        public const string RefreshTokenExpiryDays = "RefreshTokenExpiryDays";
    }
}
