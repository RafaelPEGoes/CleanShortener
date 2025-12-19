using CleanShortener.BDD.Demo.Authentication;
using CleanShortener.BDD.Demo.Configuration;
using CleanShortener.BDD.Demo.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Reqnroll;
using Reqnroll.BoDi;

namespace CleanShortener.BDD.Demo;

[Binding]
public class DependencyInjectionHook(IObjectContainer diContainer)
{
    private readonly IObjectContainer _diContainer = diContainer;

    [BeforeScenario]
    public void RegisterServices()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .AddEnvironmentVariables()
            .Build();
        var automationUser = new User();
        configuration.GetSection("AutomationUser").Bind(automationUser);

        var serviceUrls = new ServiceUrls();
        configuration.GetSection("ServiceUrls").Bind(serviceUrls);
        _diContainer.RegisterInstanceAs<IOptions<ServiceUrls>>(Options.Create(serviceUrls));
        _diContainer.RegisterInstanceAs<IOptions<User>>(Options.Create(automationUser));
        _diContainer.RegisterInstanceAs<IConfiguration>(configuration);
        _diContainer.RegisterTypeAs<HttpClientHelper, IHttpClientHelper>();
        _diContainer.RegisterTypeAs<TestContext, ITestContext>();
    }
}
