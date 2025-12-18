using CleanShortener.Domain;

namespace CleanShortener.Application;

public interface IUrlShortenerHandler
{
    public Result<ShortUrlResponse, ValidationErrors> CreateShortUrl(ShortUrlRequest shortUrlRequest);

    public ShortUrlResponse GetShortenedUrl(ShortUrlRequest shortUrlRequest);

    public ShortUrlResponse GetShortenedUrlById(string shortUrlId);
}