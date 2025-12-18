using Microsoft.AspNetCore.Mvc;
using System.Net;
using CleanShortener.Application;
using CleanShortener.Domain;
using System.Diagnostics.Metrics;

namespace CleanShortener.Controllers;

[ApiController]
[Route("/")]
public class UrlShortenerController : ControllerBase
{
    private readonly IUrlShortenerHandler _urlShortenerHandler;
    private readonly IMeterFactory _meterFactory;
    private readonly Counter<long> _requests;

    public UrlShortenerController(IUrlShortenerHandler urlShortenerService, IMeterFactory meterFactory)
    {
        _urlShortenerHandler = urlShortenerService;
        _meterFactory = meterFactory;
        _requests = _meterFactory
            .Create("UrlShortenerController")
            .CreateCounter<long>("Requests");
    }

    [HttpPost("/create")]
    [ProducesResponseType(type: typeof(ShortUrlResponse), statusCode: (int)HttpStatusCode.Created, contentType: "application/json")]
    [ProducesResponseType(type: typeof(ValidationErrors), statusCode: (int)HttpStatusCode.NotFound, contentType: "application/json")]
    public async Task<IActionResult> Create([FromBody] ShortUrlRequest urlRequest)
    {
        var createUrlResult = await _urlShortenerHandler.CreateShortUrlAsync(urlRequest);

        if (!createUrlResult.IsSuccess)
        {
            return BadRequest(createUrlResult.ValidationErrors);
        }

        var response = createUrlResult.Entity;

        _requests.Add(1);

        return Created(response!.ShortUrl, response);
    }

    [HttpGet("/{shortUrlId}")]
    [ProducesResponseType((int)HttpStatusCode.Redirect)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GoToUrl([FromRoute] string shortUrlId)
    {
        var destinationUrl = await _urlShortenerHandler.GetShortenedUrlByIdAsync(shortUrlId);

        if (destinationUrl == null)
            return NotFound();

        _requests.Add(1);

        return Redirect(destinationUrl.OriginalUrl);
    }
}