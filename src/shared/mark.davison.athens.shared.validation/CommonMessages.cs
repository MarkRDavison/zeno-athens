namespace mark.davison.athens.shared.validation;

public static class CommonMessages
{
    public const string ERROR_SAVING = nameof(ERROR_SAVING);
    public const string FAILED_TO_FIND_ENTITY = nameof(FAILED_TO_FIND_ENTITY);
    public const string INVALID_PROPERTY = nameof(INVALID_PROPERTY);

    public static string FormatMessageParameters(string message, params string[] parameters)
    {
        if (parameters.Length == 0)
        {
            return message;
        }
        var parametersSegment = string.Join('&', parameters);
        return message + '&' + parametersSegment;
    }
}
