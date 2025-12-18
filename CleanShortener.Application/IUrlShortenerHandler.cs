using CleanShortener.Domain;
using CleanShortener.Domain.ValueObjects;

namespace CleanShortener.Application;

public interface IUrlShortenerHandler
{
    public Task<Either<ShortUrlResponse, ValidationErrors>> CreateShortUrlAsync(ShortUrlRequest shortUrlRequest);

    public Task<ShortUrlResponse> GetShortenedUrlAsync(ShortUrlRequest shortUrlRequest);

    public Task<ShortUrlResponse> GetShortenedUrlByIdAsync(string shortUrlId);
}