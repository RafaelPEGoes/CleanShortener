namespace CleanShortener.BDD.Demo.Helpers;

public interface IHttpClientHelper
{
    Task<ApiResponse<TResponse>> PostAsync<TRequest, TResponse>(string endpoint, TRequest request);
}
