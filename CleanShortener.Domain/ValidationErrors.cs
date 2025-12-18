namespace CleanShortener.Domain;

public class ValidationErrors(IList<string> collectedErrors)
{
    public ValidationErrors(string singleError) : this([singleError]) { }

    public bool HasErrors { get => Errors.Count > 0; }

    public IReadOnlyList<string> Errors { get; init; } = [.. collectedErrors];
}
