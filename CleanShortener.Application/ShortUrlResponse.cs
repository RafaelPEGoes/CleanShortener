using CleanShortener.Domain;

namespace CleanShortener.Application;

public class ShortUrlResponse
{
    public string OriginalUrl { get; set; }

    public string ShortUrl { get; set; }

    public ShortUrlResponse()
    {

    }

    public ShortUrlResponse(ShortUrl shortUrl)
    {
        OriginalUrl = shortUrl.OriginalUrl;
        ShortUrl = shortUrl.ShortenedUrl;
    }
}
