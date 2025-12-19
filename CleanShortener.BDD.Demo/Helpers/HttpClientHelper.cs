using CleanShortener.BDD.Demo.Configuration;
using System.Net.Http.Json;
using System.Text.Json;

namespace CleanShortener.BDD.Demo.Helpers;

public class HttpClientHelper : IHttpClientHelper
{
    private readonly ApiTestContext _apiContext;

    public HttpClientHelper(ApiTestContext apiContext)
    {
        _apiContext = apiContext;
    }
    public async Task<TResponse> PostAsync<TRequest, TResponse>(string endpoint, TRequest request)
    {
        using var rawResponse = await _apiContext.HttpClient.PostAsJsonAsync(endpoint, request);

        rawResponse.EnsureSuccessStatusCode();

        return await rawResponse.Content.ReadFromJsonAsync<TResponse>()! ?? throw new InvalidOperationException($"HttpResponse from {endpoint} was null.");
    }
}
