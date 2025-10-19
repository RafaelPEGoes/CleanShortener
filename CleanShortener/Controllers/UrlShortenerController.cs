using Microsoft.AspNetCore.Mvc;
using System.Net;
using CleanShortener.Application;
using CleanShortener.Domain;
using System.Configuration;

namespace CleanShortener.Controllers;

[ApiController]
[Route("/")]
public class UrlShortenerController : ControllerBase
{
    private readonly IUrlShortenerHandler _urlShortenerHandler;

    public UrlShortenerController(IUrlShortenerHandler urlShortenerService)
    {
        _urlShortenerHandler = urlShortenerService;
    }

    [HttpPost("/create")]
    [ProducesResponseType(type: typeof(ShortUrlResponse), statusCode: (int)HttpStatusCode.Created, contentType: "application/json")]
    [ProducesResponseType(type: typeof(ValidationErrors), statusCode: (int)HttpStatusCode.NotFound, contentType: "application/json")]
    public IActionResult Create([FromBody] ShortUrlRequest urlRequest)
    {
        var shortUrlDto = urlRequest.ToShortUrlDto();

        var createUrlResult = _urlShortenerHandler.CreateShortUrl(shortUrlDto);

        if (!createUrlResult.IsSuccess)
        {
            return BadRequest(createUrlResult.ValidationErrors);
        }

        var response = createUrlResult.Entity!.ToShortUrlResponse();
        
        return Ok(response);
    }

    [HttpGet("/{shortUrlId}")]
    [ProducesResponseType((int)HttpStatusCode.Redirect)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public IActionResult GoToUrl([FromRoute] string shortUrlId)
    {
        var destinationUrl = _urlShortenerHandler.GetShortenedUrlById(shortUrlId);

        if (destinationUrl == null)
            return NotFound();

        return Redirect(destinationUrl.OriginalUrl);
    }
}