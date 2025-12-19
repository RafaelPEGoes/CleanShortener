namespace CleanShortener.BDD.Demo.Configuration;

internal class ApiTestContext : IDisposable
{
    public ApiFactory ApiFactory { get; set; }
    public HttpClient HttpClient { get; init; }

    public ApiTestContext()
    {
        ApiFactory = new ApiFactory();
        HttpClient = ApiFactory.CreateClient();
    }
    public void Dispose()
    {
        ApiFactory.Dispose();
        HttpClient.Dispose();
    }
}
