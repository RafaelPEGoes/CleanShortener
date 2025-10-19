namespace CleanShortener.Domain;

public class ValidationErrors
{
    public bool HasErrors { get => Errors.Count > 0; }

    public IReadOnlyList<string> Errors { get => _errors; }

    private readonly List<string> _errors = [];

    public ValidationErrors()
    {
        _errors = [];
    }

    public void Add(string error) => _errors.Add(error);
}
