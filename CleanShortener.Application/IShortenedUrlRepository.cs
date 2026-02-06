namespace CleanShortener.Application;

using CleanShortener.Domain;

public interface IShortenedUrlRepository
{
    public Task<ShortUrl> GetByDestinationUrlAsync(string destination);

    public Task<ShortUrl> SaveAsync(ShortUrl url);

    public Task<ShortUrl> GetShortenedUrlByIdAsync(string shortUrlId);

    public Task DeleteAsync(ShortUrl shortUrl);
}
