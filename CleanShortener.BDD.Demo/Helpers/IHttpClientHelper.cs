using Microsoft.AspNetCore.Identity.Data;

namespace CleanShortener.BDD.Demo.Helpers;

public interface IHttpClientHelper
{
    Task<ApiResponse<TResponse>> PostAsync<TRequest, TResponse>(string endpoint, TRequest request);

    Task<ApiResponse<TResponse>> GetAsync<TResponse>(string resource);
}
