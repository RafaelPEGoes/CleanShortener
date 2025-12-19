using CleanShortener.BDD.Demo.Configuration;
using Reqnroll;
using System.Net.Http.Json;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authentication.BearerToken;

namespace CleanShortener.BDD.Demo.Helpers;

public class HttpClientHelper : IHttpClientHelper
{
    private readonly ApiTestContext _apiContext;
    private readonly ScenarioContext _scenarioContext;

    public HttpClientHelper(ApiTestContext apiContext, ScenarioContext scenarioContext)
    {
        _apiContext = apiContext;
        _scenarioContext = scenarioContext;
    }
    public async Task<ApiResponse<TResponse>> PostAsync<TRequest, TResponse>(string endpoint, TRequest request)
    {
        return await AuthorizedPostAsync<TRequest, TResponse>(endpoint, request);
    }

    public async Task<ApiResponse<TResponse>> GetAsync<TResponse>(string resource)
    {
        if (_scenarioContext.TryGetValue<AccessTokenResponse>(ContextKeys.AccessTokenResponse, out var authnTokenResponse))
        {
            _apiContext.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authnTokenResponse.AccessToken);
        }

        using var rawResponse = await _apiContext.HttpClient.GetAsync(resource);

        rawResponse.EnsureSuccessStatusCode();

        return await ProcessResponse<TResponse>(rawResponse);
    }

    private async Task<ApiResponse<TResponse>> AuthorizedPostAsync<TRequest, TResponse>(string endpoint, TRequest request)
    {
        if (_scenarioContext.TryGetValue<AccessTokenResponse>(ContextKeys.AccessTokenResponse, out var authnTokenResponse))
        {
            _apiContext.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authnTokenResponse.AccessToken);
        }

        using var rawResponse = await _apiContext.HttpClient.PostAsJsonAsync(endpoint, request);

        rawResponse.EnsureSuccessStatusCode();

        return await ProcessResponse<TResponse>(rawResponse);
    }

    private static async Task<ApiResponse<TResponse>> ProcessResponse<TResponse>(HttpResponseMessage? rawResponse)
    {
        var parsedResponseBody = await rawResponse!.Content.ReadFromJsonAsync<TResponse>();

        return new ApiResponse<TResponse>
        {
            HttpResponse = rawResponse,
            ParsedResponseBody = parsedResponseBody!
        };
    }
}
