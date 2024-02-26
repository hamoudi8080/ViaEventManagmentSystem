using ViaEventManagmentSystem.Core.Tools.OperationResult;

public class Result
{
    public bool IsSuccess { get; }
    public List<Error> Errors { get; }

    protected Result(bool isSuccess, List<Error> errors)
    {
        IsSuccess = isSuccess;
        Errors = errors;
    }

    public static Result Success()
    {
        return new Result(true, new List<Error>());
    }

    public static Result Failure(Error error)
    {
        return new Result(false, new List<Error> { error });
    }

    public static Result Failure(List<Error> errors)
    {
        return new Result(false, errors);
    }

    public static Result Combine(params Result[] results)
    {
        var combinedErrors = new List<Error>();
        foreach (var result in results)
        {
            combinedErrors.AddRange(result.Errors);
        }
        return new Result(combinedErrors.Count == 0, combinedErrors);
    }

    public static implicit operator Result(bool success)
    {
        return success ? Success() : Failure(new Error(0, "Operation failed."));
    }
}

public class Result<T> : Result
{
    public T Value { get; }

    protected Result(bool isSuccess, T value, List<Error> errors) : base(isSuccess, errors)
    {
        Value = value;
    }

    public static Result<T> Success(T value)
    {
        return new Result<T>(true, value, new List<Error>());
    }

    public static new Result<T> Failure(Error error)
    {
        return new Result<T>(false, default, new List<Error> { error });
    }

    public static new Result<T> Failure(List<Error> errors)
    {
        return new Result<T>(false, default, errors);
    }

    public static Result<T> Combine(params Result<T>[] results)
    {
        var combinedErrors = new List<Error>();
        T value = default;
        foreach (var result in results)
        {
            if (!result.IsSuccess)
            {
                combinedErrors.AddRange(result.Errors);
            }
            else
            {
                value = result.Value;
            }
        }
        return new Result<T>(combinedErrors.Count == 0, value, combinedErrors);
    }

    public static implicit operator Result<T>(T value)
    {
        return Success(value);
    }
}