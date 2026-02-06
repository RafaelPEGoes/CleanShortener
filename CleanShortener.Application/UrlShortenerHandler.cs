using CleanShortener.Domain;
using CleanShortener.Domain.ValueObjects;
using Microsoft.Extensions.Configuration;

namespace CleanShortener.Application;

public class UrlShortenerHandler : IUrlShortenerHandler
{
    private readonly IShortenedUrlDataProxy _urlDataProxy;
    private readonly IConfiguration _configuration;

    public UrlShortenerHandler(IShortenedUrlDataProxy urlDataRepository, IConfiguration configuration)
    {
        _urlDataProxy = urlDataRepository;
        _configuration = configuration;
    }

    public async Task<Either<ShortUrlResponse, ValidationErrors>> CreateShortUrlAsync(ShortUrlRequest shortUrlRequest)
    {
        var internalUrl = _configuration["BaseUrl"]!;

        var persistedUrl = await GetShortenedUrlAsync(shortUrlRequest);

        if (persistedUrl is not null)
            return Either<ShortUrlResponse, ValidationErrors>.Of(persistedUrl);

        var result = ShortUrl.TryCreate(shortUrlRequest.Url, internalUrl);

        if (result.IsFailure)
            return result.Transform<ShortUrl, ShortUrlResponse>(result.ValidationErrors!);

        await _urlDataProxy.SaveAsync(result.Entity!);

        return result.Transform<ShortUrl, ShortUrlResponse>(new ShortUrlResponse(result.Entity!));
    }

    public async Task DeleteByIdAsync(string shortUrlId)
    {
        var internalUrl = _configuration["BaseUrl"]!;

        var fullShortenedUrl = $"{internalUrl}/{shortUrlId}";

        var persistedUrl = await _urlDataProxy.GetShortenedUrlByIdAsync(fullShortenedUrl);

        if (persistedUrl is null)
            return;

        await _urlDataProxy.DeleteAsync(persistedUrl);
    }

    public async Task<ShortUrlResponse> GetShortenedUrlAsync(ShortUrlRequest shortUrlRequest)
    {
        var persistedUrl = await _urlDataProxy.GetByDestinationUrlAsync(shortUrlRequest.Url);

        if (persistedUrl is null)
            return null!;

        return new ShortUrlResponse(persistedUrl);
    }

    public async Task<ShortUrlResponse> GetShortenedUrlByIdAsync(string shortUrlId)
    {
        var internalUrl = _configuration["BaseUrl"]!;

        var fullShortenedUrl = $"{internalUrl}/{shortUrlId}";

        var persistedUrl = await _urlDataProxy.GetShortenedUrlByIdAsync(fullShortenedUrl);

        if (persistedUrl is null)
            return null!;

        return new ShortUrlResponse(persistedUrl);
    }
}