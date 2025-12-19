namespace CleanShortener.Domain;

public abstract class GenericResult<T, U>
    where T : class
    where U : class
{
    public bool IsSuccess { get; init; }

    protected T? This { get; init; }

    protected U? That { get; init; }
}