using CleanShortener.Domain;

namespace CleanShortener.Application;

public interface IUrlShortenerHandler
{
    public Result<ShortUrlDto, ValidationErrors> CreateShortUrl(ShortUrlRequest shortUrlRequest);

    public ShortUrlDto GetShortenedUrl(ShortUrlDto shortUrlDto);

    public ShortUrlDto GetShortenedUrlById(string shortUrlId);
}