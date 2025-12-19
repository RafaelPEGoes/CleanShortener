namespace CleanShortener.BDD.Demo;

public class ApiResponse<TResponse>
{
    public required TResponse ParsedResponseBody { get; init; }

    public required HttpResponseMessage HttpResponse { get; init; }
}
