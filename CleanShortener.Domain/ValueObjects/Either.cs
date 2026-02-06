using System.Diagnostics.CodeAnalysis;

namespace CleanShortener.Domain.ValueObjects;

public class Either<T, U> : GenericResult<T, U>
    where T : class
    where U : ValidationErrors
{
    private Either(T? entity = null, U? validationErrors = null)
    {
        This = entity;
        That = validationErrors;
        IsSuccess = validationErrors == null;
    }

    public static Either<T, U> Of(T entity) => new(entity: entity);

    public static Either<T, U> Of(U validationErrors) => new(validationErrors: validationErrors);

    public bool IsFailure { get => !IsSuccess; }

    public T? Entity { get => This; }

    public U? ValidationErrors { get => That; }
}

[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "Removing the object which the extension methods operates upon (obviously) break the implementation.")]
public static class EitherExtensions
{
    /// <summary>
    /// Maps a <see cref="Either{T, U}"/> to a new type.
    /// </summary>
    /// <typeparam name="T">The old type</typeparam>
    /// <typeparam name="TNew">The new type the <see cref="Either{T, TNew}"/> will be casted to.</typeparam>
    /// <returns></returns>
    public static Either<TNew, ValidationErrors> Transform<T, TNew>(this Either<T, ValidationErrors> original, TNew newEntity)
        where T : class
        where TNew : class
    {
        return Either<TNew, ValidationErrors>.Of(newEntity);
    }

    /// <summary>
    /// Maps a <see cref="Either{T, U}"/> to a new type.
    /// </summary>
    /// <typeparam name="T">The old type</typeparam>
    /// <typeparam name="TNew">The new type the <see cref="Either{T, TNew}"/> will be casted to.</typeparam>
    /// <returns></returns>
    public static Either<TNew, ValidationErrors> Transform<T, TNew>(this Either<T, ValidationErrors> original, ValidationErrors errors)
        where T : class
        where TNew : class
    {
        return Either<TNew, ValidationErrors>.Of(errors);
    }
}