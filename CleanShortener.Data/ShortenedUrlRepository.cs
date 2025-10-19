using CleanShortener.Application;
using CleanShortener.Domain;
using System.Collections.Concurrent;

namespace CleanShortener.Data;

public class ShortenedUrlRepository : IShortenedUrlRepository
{
    private static ConcurrentBag<ShortUrl> _database = new();

    public ShortUrl Save(ShortUrl url)
    {
        _database.Add(url);

        return url;
    }

    public ShortUrl GetByDestinationUrl(string destination)
    {
        return _database
            .Where(u => u.OriginalUrl == destination)
            .FirstOrDefault()!;
    }

    public ShortUrl GetShortenedUrlById(string shortUrlId)
    {
        return _database
            .Where(u => u.ShortenedUrl == shortUrlId)
            .FirstOrDefault()!;
    }
}