namespace CarRentalZaimi.Application.Common.Errors;

public static class ErrorMessages
{
    private static readonly Dictionary<string, Dictionary<string, string>> Messages = new()
    {
        // English Messages
        ["en"] = new Dictionary<string, string>
        {
            // General Errors
            [ErrorCodes.NOT_FOUND] = "The requested resource was not found",
            [ErrorCodes.ALREADY_EXISTS] = "The resource already exists",
            [ErrorCodes.INVALID_OPERATION] = "The operation is not valid",
            [ErrorCodes.UNAUTHORIZED] = "You are not authorized to perform this action",
            [ErrorCodes.FORBIDDEN] = "Access to this resource is forbidden",
            [ErrorCodes.VALIDATION_FAILED] = "Validation failed",
            [ErrorCodes.CONCURRENT_UPDATE] = "The resource was modified by another user",
            [ErrorCodes.EXTERNAL_SERVICE_ERROR] = "An external service error occurred",
            [ErrorCodes.DATABASE_ERROR] = "A database error occurred while processing your request",

            // User Errors
            [ErrorCodes.USER_NOT_FOUND] = "User not found",
            [ErrorCodes.USER_ALREADY_EXISTS] = "User already exists",
            [ErrorCodes.USER_INACTIVE] = "User account is inactive",
            [ErrorCodes.USER_EMAIL_ALREADY_EXISTS] = "Email address is already registered",
            [ErrorCodes.USER_PHONE_ALREADY_EXISTS] = "Phone number is already registered",
            [ErrorCodes.USER_INVALID_CREDENTIALS] = "Invalid email or password",
            [ErrorCodes.USER_PROFILE_NOT_FOUND] = "User profile not found",

        },

        // Albanian Messages
        ["sq"] = new Dictionary<string, string>
        {
            // General Errors
            [ErrorCodes.NOT_FOUND] = "Burimi i kërkuar nuk u gjet",
            [ErrorCodes.ALREADY_EXISTS] = "Burimi ekziston tashmë",
            [ErrorCodes.INVALID_OPERATION] = "Operacioni nuk është i vlefshëm",
            [ErrorCodes.UNAUTHORIZED] = "Nuk jeni të autorizuar për të kryer këtë veprim",
            [ErrorCodes.FORBIDDEN] = "Qasja në këtë burim është e ndaluar",
            [ErrorCodes.VALIDATION_FAILED] = "Validimi dështoi",
            [ErrorCodes.CONCURRENT_UPDATE] = "Burimi u modifikua nga një përdorues tjetër",
            [ErrorCodes.EXTERNAL_SERVICE_ERROR] = "Ndodhi një gabim në shërbimin e jashtëm",
            [ErrorCodes.DATABASE_ERROR] = "Ndodhi një gabim në bazën e të dhënave gjatë përpunimit të kërkesës suaj",

            // User Errors
            [ErrorCodes.USER_NOT_FOUND] = "Përdoruesi nuk u gjet",
            [ErrorCodes.USER_ALREADY_EXISTS] = "Përdoruesi ekziston tashmë",
            [ErrorCodes.USER_INACTIVE] = "Llogaria e përdoruesit është joaktive",
            [ErrorCodes.USER_EMAIL_ALREADY_EXISTS] = "Adresa e email-it është regjistruar tashmë",
            [ErrorCodes.USER_PHONE_ALREADY_EXISTS] = "Numri i telefonit është regjistruar tashmë",
            [ErrorCodes.USER_INVALID_CREDENTIALS] = "Email ose fjalëkalim i pavlefshëm",
            [ErrorCodes.USER_PROFILE_NOT_FOUND] = "Profili i përdoruesit nuk u gjet",
        },

        // Spanish Messages
        ["es"] = new Dictionary<string, string>
        {
            // General Errors
            [ErrorCodes.NOT_FOUND] = "El recurso solicitado no fue encontrado",
            [ErrorCodes.ALREADY_EXISTS] = "El recurso ya existe",
            [ErrorCodes.INVALID_OPERATION] = "La operación no es válida",
            [ErrorCodes.UNAUTHORIZED] = "No está autorizado para realizar esta acción",
            [ErrorCodes.FORBIDDEN] = "El acceso a este recurso está prohibido",
            [ErrorCodes.VALIDATION_FAILED] = "La validación falló",
            [ErrorCodes.CONCURRENT_UPDATE] = "El recurso fue modificado por otro usuario",
            [ErrorCodes.EXTERNAL_SERVICE_ERROR] = "Ocurrió un error en el servicio externo",
            [ErrorCodes.DATABASE_ERROR] = "Ocurrió un error en la base de datos al procesar su solicitud",

            // User Errors
            [ErrorCodes.USER_NOT_FOUND] = "Usuario no encontrado",
            [ErrorCodes.USER_ALREADY_EXISTS] = "El usuario ya existe",
            [ErrorCodes.USER_INACTIVE] = "La cuenta de usuario está inactiva",
            [ErrorCodes.USER_EMAIL_ALREADY_EXISTS] = "La dirección de correo electrónico ya está registrada",
            [ErrorCodes.USER_PHONE_ALREADY_EXISTS] = "El número de teléfono ya está registrado",
            [ErrorCodes.USER_INVALID_CREDENTIALS] = "Correo electrónico o contraseña inválidos",
            [ErrorCodes.USER_PROFILE_NOT_FOUND] = "Perfil de usuario no encontrado",

        }
    };

    public static string GetMessage(string errorCode, string language = "en")
    {
        if (!Messages.ContainsKey(language))
            language = "en"; // Fallback to English

        if (!Messages[language].ContainsKey(errorCode))
            return $"Error message not found for code: {errorCode}";

        return Messages[language][errorCode];
    }

    public static IEnumerable<string> GetSupportedLanguages()
    {
        return Messages.Keys;
    }
}
