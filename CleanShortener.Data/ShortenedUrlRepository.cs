using CleanShortener.Application;
using CleanShortener.Data.DbContexts;
using CleanShortener.Domain;
using Microsoft.EntityFrameworkCore;

namespace CleanShortener.Data;

public class ShortenedUrlRepository(CleanShortenerDbContext database) : IShortenedUrlRepository
{
    private readonly CleanShortenerDbContext _database = database;

    public async Task<ShortUrl> SaveAsync(ShortUrl url)
    {
        await _database.ShortUrls.AddAsync(url);

        var isSaved = await _database.SaveChangesAsync();

        return url;
    }

    public async Task<ShortUrl> GetByDestinationUrlAsync(string destination)
    {
        return await _database.ShortUrls.FirstOrDefaultAsync(u => u.OriginalUrl == destination);
    }

    public async Task<ShortUrl> GetShortenedUrlByIdAsync(string shortUrlId)
    {
        return await _database.ShortUrls.FirstOrDefaultAsync(u => u.ShortenedUrl == shortUrlId);
    }
}