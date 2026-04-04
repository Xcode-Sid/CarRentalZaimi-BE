namespace CarRentalZaimi.Application.Common.Phone;

public class PhoneSettings
{
    public string ServiceName { get; set; } = string.Empty;
    public string TwilioAccountSid { get; set; } = string.Empty;
    public string TwilioAuthToken { get; set; } = string.Empty;
    public string TwilioPhoneNumber { get; set; } = string.Empty;
    public bool EnableSms { get; set; } = true;
    public int MaxMessageLength { get; set; } = 1600;
    public int CodeExpiryMinutes { get; set; } = 10;
}
