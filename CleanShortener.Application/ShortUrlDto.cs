using CleanShortener.Domain;

namespace CleanShortener.Application;

public class ShortUrlDto
{
    public string OriginalUrl { get; set; }

    public string ShortenedUrl { get; set; }

    public static ShortUrlDto ToShortUrlDTO(ShortUrlRequest shortUrlRequest)
    {
        return new ShortUrlDto()
        {
            OriginalUrl = shortUrlRequest.Url,
        };
    }

    public static ShortUrlResponse ToShortUrlResponse(ShortUrlDto shortUrlDto)
    {
        return new ShortUrlResponse()
        {
            OriginalUrl = shortUrlDto.OriginalUrl,
            ShortUrl = shortUrlDto.ShortenedUrl
        };
    }
}

public static class ShortUrlDtoExtensions
{
    public static ShortUrlDto ToShortUrlDto(this ShortUrl shortUrl)
    {
        if (shortUrl is null)
            return default!;

        return new ShortUrlDto
        {
            OriginalUrl = shortUrl.OriginalUrl,
            ShortenedUrl = shortUrl.ShortenedUrl
        };
    }

    public static ShortUrlDto ToShortUrlDto(this ShortUrlRequest shortUrlRequest)
    {
        if (shortUrlRequest is null)
            return default!;

        return new ShortUrlDto
        {
            OriginalUrl = shortUrlRequest.Url
        };
    }

    public static ShortUrlResponse ToShortUrlResponse(this ShortUrlDto shortUrlDto)
    {
        if (shortUrlDto is null)
            return default!;

        return new ShortUrlResponse
        {
            OriginalUrl = shortUrlDto.OriginalUrl,
            ShortUrl = shortUrlDto.ShortenedUrl
        };
    }
}