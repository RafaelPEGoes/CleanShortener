using CleanShortener.BDD.Demo.Helpers;
using Microsoft.AspNetCore.Authentication.BearerToken;

namespace CleanShortener.BDD.Demo.Configuration;

public interface ITestContext
{
    public ServiceUrls ServiceUrls { get; }
    public IHttpClientHelper HttpClient { get; }
    public AccessTokenResponse? AccessTokenResponse { get; set; }
}
