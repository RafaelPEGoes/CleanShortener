using Microsoft.Extensions.Caching.Memory;
using CleanShortener.Application;
using CleanShortener.Domain;
using System.Diagnostics.CodeAnalysis;

namespace CleanShortener.Data;

[ExcludeFromCodeCoverage]
public class ShortenedUrlDataProxy : IShortenedUrlDataProxy
{
    private readonly IMemoryCache _cache;
    private readonly IShortenedUrlRepository _repository;

    public ShortenedUrlDataProxy(
        IMemoryCache cache,
        IShortenedUrlRepository repository)
    {
        _cache = cache;
        _repository = repository;
    }

    public ShortUrl GetByDestinationUrl(string destination)
    {
        if (_cache.TryGetValue<ShortUrl>(destination, out var shortenedUrl) && shortenedUrl is not null)
            return shortenedUrl;
        
        var persistedUrl = _repository.GetByDestinationUrl(destination);

        if (persistedUrl is not null)
            PersistEntries(persistedUrl);

        return persistedUrl!;
    }

    public ShortUrl GetShortenedUrlById(string shortUrlId)
    {
        if (_cache.TryGetValue<ShortUrl>(shortUrlId, out var shortenedUrl) && shortenedUrl is not null)
            return shortenedUrl;

        var persistedUrl = _repository.GetByDestinationUrl(shortUrlId);

        if (persistedUrl is not null)
            PersistEntries(persistedUrl);
        
        return persistedUrl!;
    }

    public ShortUrl Save(ShortUrl shortUrl)
    {
        _repository.Save(shortUrl);

        PersistEntries(shortUrl);

        return shortUrl;
    }

    private void PersistEntries(ShortUrl shortUrl)
    {
        _cache.Set<ShortUrl>(shortUrl.OriginalUrl, shortUrl);
        _cache.Set<ShortUrl>(shortUrl.ShortenedUrl, shortUrl);
    }
}