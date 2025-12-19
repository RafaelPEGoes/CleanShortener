namespace CleanShortener.Domain.ValueObjects;

internal static class UrlCreationErrors
{
    public static string NullOrEmpty() => "URL is null or empty.";
    public static string Invalid(string url) => $"{url} is not a valid URL.";
    public static string InvalidProtocol(IReadOnlyList<string> acceptedProtocols) => $"URL does not use a valid protocol. Accepted values are {string.Join(",", acceptedProtocols)}.";
}
