using CleanShortener.Domain;

namespace CleanShortener.Application;

public class UrlShortenerHandler : IUrlShortenerHandler
{
    private readonly IShortenedUrlDataProxy _urlDataProxy;

    public UrlShortenerHandler(IShortenedUrlDataProxy urlDataRepository)
    {
        _urlDataProxy = urlDataRepository;
    }

    public Result<ShortUrlResponse, ValidationErrors> CreateShortUrl(ShortUrlRequest shortUrlRequest)
    {
        // TODO: normalize URL, verify if it is reachable using a Specification, make it async etc...
        var validationResult = ShortUrl.Validate(shortUrlRequest.Url);

        if (validationResult.HasErrors)
            return Result<ShortUrlResponse, ValidationErrors>.Build(validationResult);

        var persistedUrl = GetShortenedUrl(shortUrlRequest);
        
        if (persistedUrl is not null)
            return Result<ShortUrlResponse, ValidationErrors>.Build(persistedUrl);

        var shortUrl = new ShortUrl(shortUrlRequest.Url);

        _urlDataProxy.Save(shortUrl);

        var response = new ShortUrlResponse(shortUrl);

        return Result<ShortUrlResponse, ValidationErrors>.Build(response);
    }

    public ShortUrlResponse GetShortenedUrl(ShortUrlRequest shortUrlRequest)
    {
        var persistedUrl = _urlDataProxy.GetByDestinationUrl(shortUrlRequest.Url);

        return new ShortUrlResponse(persistedUrl);
    }

    public ShortUrlResponse GetShortenedUrlById(string shortUrlId)
    {
        // convert cache to ValueTask, it will most likely be completed syncronously 
        var persistedUrl = _urlDataProxy.GetShortenedUrlById(shortUrlId);

        return new ShortUrlResponse(persistedUrl);
    }
}