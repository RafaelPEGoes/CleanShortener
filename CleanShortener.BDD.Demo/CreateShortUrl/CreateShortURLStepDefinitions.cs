using CleanShortener.Application;
using Reqnroll;
using CleanShortener.BDD.Demo.Configuration;

namespace CleanShortener.BDD.Demo.CreateShortUrl;

[Binding]
public class CreateShortUrlStepDefinitions(ScenarioContext scenarioContext,
    ITestContext testContext)
{
    private readonly ScenarioContext _scenarioContext = scenarioContext;
    private readonly ITestContext _testContext = testContext;

    [Given("the url {string}")]
    [Given("a url {string}")]
    public void GivenTheUrl(string url)
    {
        _scenarioContext[ContextKeys.LongUrl] = url;
    }

    [When("I request a shortened url")]
    [When("eu solicito uma url encurtada")]
    public async Task WhenIRequestAShortenedURLAsync()
    {
        var longUrl = _scenarioContext.Get<string>(ContextKeys.LongUrl);

        var createShortUrlRequest = new ShortUrlRequest()
        {
            Url = longUrl,
        };

        var createUrlEndpoint = _testContext.ServiceUrls.CreateShortUrl;
        var createUrlResponse = await _testContext.HttpClient.PostAsync<ShortUrlRequest, ShortUrlResponse>(createUrlEndpoint, createShortUrlRequest);

        Assert.NotNull(createUrlEndpoint);

        _scenarioContext[ContextKeys.CreateShortUrlResponse] = createUrlResponse;
    }

    [Then("I receive a shortened url in response")]
    [Then("eu recebo uma url encurtada de volta")]
    public void IReceiveAShortenedUrlInResponse()
    {
        var createUrlResponse = _scenarioContext.Get<ApiResponse<ShortUrlResponse>>(ContextKeys.CreateShortUrlResponse);

        Assert.Equal(201, (int)createUrlResponse.HttpResponse.StatusCode);

        var parsedResponse = createUrlResponse.ParsedResponseBody;
        var originalUrl = _scenarioContext.Get<string>(ContextKeys.LongUrl);

        Assert.NotEqual(parsedResponse.ShortUrl, originalUrl);
    }
}
