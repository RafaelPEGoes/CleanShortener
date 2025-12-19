using CleanShortener.BDD.Demo.Configuration;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.Extensions.Options;
using Reqnroll;

namespace CleanShortener.BDD.Demo.Authentication;

[Binding]
public class AuthenticationHook(ScenarioContext scenarioContext,
    IOptions<User> automationUser,
    ITestContext testContext
    )
{
    private readonly ScenarioContext _scenarioContext = scenarioContext;
    private readonly IOptions<User> _automationUser = automationUser;
    private readonly ITestContext _testContext = testContext;

    [BeforeScenario]
    public async Task AuthorizeAsync()
    {
        var credentials = _automationUser.Value;

        var request = new LoginRequest()
        {
            Email = credentials.Username,
            Password = credentials.Password,
        };

        var response = await _testContext.HttpClient.PostAsync<LoginRequest, AccessTokenResponse>(_testContext.ServiceUrls.LoginEndpoint, request);

        _testContext.ServiceToken = response;
    }
}
