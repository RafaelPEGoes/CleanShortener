using CleanShortener.BDD.Demo.Configuration;
using Reqnroll;

namespace CleanShortener.BDD.Demo.GetShortUrl;

[Binding]
public class GetShortUrlStepDefinitions(ScenarioContext scenarioContext, ITestContext testContext)
{
    private readonly ScenarioContext _scenarioContext = scenarioContext;
    private readonly ITestContext _testContext = testContext;

    [Given("the shortened url bound to {string}")]
    public void GivenTheShortenedUrlBoundTo(string originalUrl)
    {
        _scenarioContext[ContextKeys.OriginalUrl] = originalUrl;
    }

    [When("I open the shortened url")]
    public void WhenIOpenTheShortenedUrl()
    {
        throw new PendingStepException();
    }

    [Then("I should be redirected to the original url")]
    public void ThenIShouldBeRedirectedToTheOriginalUrl()
    {
        throw new PendingStepException();
    }

}
