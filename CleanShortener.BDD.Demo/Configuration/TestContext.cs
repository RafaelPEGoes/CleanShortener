using CleanShortener.BDD.Demo.Helpers;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.Extensions.Options;

namespace CleanShortener.BDD.Demo.Configuration;

public class TestContext(IOptions<ServiceUrls> serviceUrls, IHttpClientHelper httpClient) : ITestContext
{
    public ServiceUrls ServiceUrls { get; } = serviceUrls.Value;
    public IHttpClientHelper HttpClient { get; } = httpClient;

    public AccessTokenResponse? ServiceToken { get; set; }
}
