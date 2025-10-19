using CleanShortener.Domain;

namespace CleanShortener.Application;

public class UrlShortenerHandler : IUrlShortenerHandler
{
    private readonly IShortenedUrlDataProxy _urlDataProxy;

    public UrlShortenerHandler(IShortenedUrlDataProxy urlDataRepository)
    {
        _urlDataProxy = urlDataRepository;
    }

    public Result<ShortUrlDto, ValidationErrors> CreateShortUrl(ShortUrlDto shortUrlDto)
    {
        // TODO: normalize URL, verify if it is reachable using a Specification, make it async etc...
        var validationResult = ShortUrl.Validate(shortUrlDto.OriginalUrl);

        if (validationResult.HasErrors)
            return Result<ShortUrlDto, ValidationErrors>.Build(validationResult);

        var persistedUrl = GetShortenedUrl(shortUrlDto);
        
        if (persistedUrl is not null)
            return Result<ShortUrlDto, ValidationErrors>.Build(persistedUrl);

        var shortUrl = new ShortUrl(shortUrlDto.OriginalUrl);

        _urlDataProxy.Save(shortUrl);

        shortUrlDto.ShortenedUrl = shortUrl.ShortenedUrl;

        return Result<ShortUrlDto, ValidationErrors>.Build(shortUrlDto);
    }

    public ShortUrlDto GetShortenedUrl(ShortUrlDto shortUrlDto)
    {
        var persistedUrl = _urlDataProxy.GetByDestinationUrl(shortUrlDto.OriginalUrl);

        return persistedUrl.ToShortUrlDto();
    }

    public ShortUrlDto GetShortenedUrlById(string shortUrlId)
    {
        // convert cache to ValueTask, it will most likely be completed syncronously 
        var persistedUrl = _urlDataProxy.GetShortenedUrlById(shortUrlId);

        return persistedUrl.ToShortUrlDto();
    }
}