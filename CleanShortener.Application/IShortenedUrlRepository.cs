namespace CleanShortener.Application;

using CleanShortener.Domain;

public interface IShortenedUrlRepository
{
    public ShortUrl GetByDestinationUrl(string destination);

    public ShortUrl Save(ShortUrl url);

    public ShortUrl GetShortenedUrlById(string shortUrlId);
}
