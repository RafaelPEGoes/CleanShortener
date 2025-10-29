namespace CleanShortener.Domain;

public class Result<T, U>
    where T : class 
    where U : ValidationErrors
{
    public bool IsSuccess { get; private set; }

    public T? Entity { get; private set; }

    public  U? ValidationErrors { get; private set; }

    private Result(T? entity, U? validationErrors, bool isSuccess)
    {
        Entity = entity;
        ValidationErrors = validationErrors;
        IsSuccess = isSuccess;
    }

    public static Result<T, U> Build(T? result) => new(result!, null, isSuccess: true);
    public static Result<T, U> Build(U? validationErrors) => new(null, validationErrors!, isSuccess: false);

}
