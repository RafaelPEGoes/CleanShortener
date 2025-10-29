namespace CleanShortener.Domain;

public class ShortUrl
{
    public string OriginalUrl { get; set; }

    public string ShortenedUrl { get; init; }

    private const int UniqueIdentifierLength = 6;

    private static readonly char[] _b36Chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

    private static readonly Random _randomFactor = new();

    private static readonly IReadOnlyList<string> _allowedUriSchemes = [Uri.UriSchemeHttp, Uri.UriSchemeHttps];

    public ShortUrl(string originalUrl)
    {
        this.OriginalUrl = originalUrl;
        this.ShortenedUrl = GetShortenedUrl();
    }

    private string GetShortenedUrl()
    {
        var uniqueIdentifier = GetUniqueIdentifierForUrl(length: UniqueIdentifierLength);

        return uniqueIdentifier;
    }

    private static string GetUniqueIdentifierForUrl(int length)
    {
        char[] output = new char[length];

        for (int i = 0; i < length; i++)
            output[i] = _b36Chars[_randomFactor.Next(_b36Chars.Length)];

        return string.Concat(output);
    }

    public static ValidationErrors Validate(string url)
    {
        ValidationErrors errors = new();

        if (string.IsNullOrEmpty(url))
            errors.Add($"URL is null or empty.");

        if (!Uri.TryCreate(url, uriKind: UriKind.Absolute, out var parsedUri))
            errors.Add($"{url} is not a valid URL.");

        if (!IsValidProtocol(parsedUri!))
            errors.Add($"URL does not use a valid protocol. Accepted values are HTTP and HTTPS.");

        return errors;
    }

    private static bool IsValidProtocol(Uri uri)
    {
        return _allowedUriSchemes.Contains(uri?.Scheme);
    }
}