using CleanShortener.Domain;

namespace CleanShortener.Application;

public interface IUrlShortenerHandler
{
    public Result<ShortUrlDto, ValidationErrors> CreateShortUrl(ShortUrlDto shortUrlRequest);

    public ShortUrlDto GetShortenedUrl(ShortUrlDto shortUrlDto);

    public ShortUrlDto GetShortenedUrlById(string shortUrlId);
}