namespace CleanShortener.BDD.Demo.Helpers;

public interface IHttpClientHelper
{
    Task<TResponse> PostAsync<TRequest, TResponse>(string endpoint, TRequest request);
}
