namespace Music.Application.HelperModels;

public class QueryResult<T>
{
    public bool IsSuccess { get; }

    public IEnumerable<string> Errors { get; }

    public T Value { get; }

    private QueryResult(T value, bool isSuccess, IEnumerable<string> errors)
    {
        Value = value;
        IsSuccess = isSuccess;
        Errors = errors ?? Enumerable.Empty<string>();
    }

    public static QueryResult<T> Success(T value)
        => new QueryResult<T>(value, true, Enumerable.Empty<string>());

    public static QueryResult<T> SuccessWithWarnings(T value, IEnumerable<string> warnings)
        => new QueryResult<T>(value, true, warnings);

    public static QueryResult<T> Failure(IEnumerable<string> errors)
        => new QueryResult<T>(default!, false, errors);

    public static QueryResult<T> FailureWithValue(T value, IEnumerable<string> errors)
        => new QueryResult<T>(value, false, errors);
}