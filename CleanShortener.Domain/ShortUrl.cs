using CleanShortener.Domain.ValueObjects;

namespace CleanShortener.Domain;

public class ShortUrl : DomainEntity
{
    public Guid Id { get; } = Guid.NewGuid();

    public string OriginalUrl { get; set; }

    // probably primitive obsession at this point but eh, what really matters is the friendships we make along the way
    public string ShortenedUrl { get; init; }

    private const int UniqueIdentifierLength = 6;

    private static readonly char[] _b36Chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

    private static readonly Random _randomFactor = new();

    private static readonly IReadOnlyList<string> _allowedUriSchemes = [Uri.UriSchemeHttp, Uri.UriSchemeHttps];

    // messed up but will stay until I figure a workaround.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public ShortUrl() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

    private ShortUrl(string originalUrl, string shortenerUrl)
    {
        this.OriginalUrl = originalUrl;
        this.ShortenedUrl = GetShortenedUrl(shortenerUrl);
    }

    private static string GetShortenedUrl(string shortenerUrl)
    {
        var uniqueIdentifier = GetUniqueIdentifierForUrl(length: UniqueIdentifierLength);

        return $"{shortenerUrl}/{uniqueIdentifier}";
    }

    private static string GetUniqueIdentifierForUrl(int length)
    {
        char[] output = new char[length];

        for (int i = 0; i < length; i++)
            output[i] = _b36Chars[_randomFactor.Next(_b36Chars.Length)];

        return string.Concat(output);
    }

    private static ValidationErrors Validate(string url, string shortenerUrl)
    {
        // still an invariant, but that's a problem that resides in the application itself
        // and holds no meaning to the final user, that's why an Exception seems more appropriate.
        ArgumentNullException.ThrowIfNullOrEmpty(shortenerUrl);

        // short-circuit because if will fail in the next checks anyway. The response
        // will be cleaner at least, although it makes the validation flow harder to reason about.
        if (string.IsNullOrWhiteSpace(url))
            return new ValidationErrors(UrlCreationErrors.NullOrEmpty());

        List<string> errors = [];
        // This is a very interesting one.
        // Clearly this is an invariant as users should not be allowed to shorten
        // links that point to the shortener itself (made up this one), but how the application should
        // go about this looking from user's perspective?
        if (url.Contains(shortenerUrl))
            errors.Add(UrlCreationErrors.Invalid(url));

        if (!Uri.TryCreate(url, uriKind: UriKind.Absolute, out var parsedUri))
            errors.Add(UrlCreationErrors.Invalid(url));

        if (!_allowedUriSchemes.Contains(parsedUri?.Scheme))
            errors.Add(UrlCreationErrors.InvalidProtocol(_allowedUriSchemes));

        return new ValidationErrors(errors);
    }

    public static Either<ShortUrl, ValidationErrors> TryCreate(string originalUrl, string shortenerUrl)
    {
        var validationErrors = Validate(originalUrl, shortenerUrl);

        return validationErrors.HasErrors
            ? Either<ShortUrl, ValidationErrors>.Of(validationErrors)
            : Either<ShortUrl, ValidationErrors>.Of(new ShortUrl(originalUrl, shortenerUrl));
    }
}