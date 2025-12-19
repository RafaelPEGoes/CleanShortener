using CleanShortener.Application;
using Reqnroll;
using System.Text.Json;
using System.Text;
using CleanShortener.BDD.Demo.Configuration;

namespace CleanShortener.BDD.Demo.CreateUrl;

[Binding]
public class CreateShortUrlStepDefinitions(ScenarioContext scenarioContext, ApiTestContext apiTestContext)
{
    private readonly ScenarioContext _scenarioContext = scenarioContext;
    private readonly ApiTestContext _apiContext = apiTestContext;

    [Given("the url {string}")]
    public void GivenTheUrl(string url)
    {
        _scenarioContext[ContextKeys.Url] = url;
    }

    [When("I request a shortened URL")]
    public async Task WhenIRequestAShortenedURLAsync()
    {
        var request = new ShortUrlRequest() { Url = (string)_scenarioContext[ContextKeys.Url] };
        var httpContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

        var response = await _apiContext.HttpClient.PostAsync("http://localhost:7777/create", httpContent);

        if (response is not null)
        {
            var contentString = await response.Content.ReadAsStringAsync();
            var shortUrlResponse = JsonSerializer.Deserialize<ShortUrlResponse>(contentString);
            //_scenarioContext[ContextKeys.CreatedResponse] = shortUrlResponse;
        }
    }

    [Then("[outcome]")]
    public void ThenOutcome()
    {
        throw new PendingStepException();
    }
}
