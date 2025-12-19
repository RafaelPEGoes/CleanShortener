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
    public async Task<ApiResponse<TResponse>> PostAsync<TRequest, TResponse>(string endpoint, TRequest request)
    {
        using var rawResponse = await _apiContext.HttpClient.PostAsJsonAsync(endpoint, request);

        rawResponse.EnsureSuccessStatusCode();

        var parsedResponseBody = await rawResponse.Content.ReadFromJsonAsync<TResponse>();

        return new ApiResponse<TResponse>
        {
            HttpResponse = rawResponse,
            ParsedResponseBody = parsedResponseBody!
        };
    }
}
