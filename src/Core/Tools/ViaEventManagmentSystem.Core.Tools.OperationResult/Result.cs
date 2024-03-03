using ViaEventManagmentSystem.Core.Tools.OperationResult;

public class Result
{
    public bool IsSuccess { get; }
    public Error Error { get; }

    protected Result(bool isSuccess, Error error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Success()
    {
        return new Result(true,  Error.NotFoundError);
    }

    public static Result Failure(Error error)
    {
        return new Result(false, error);
    }



    public static  Result Combine(params Result[] results)
    {
        var combinedErrors = new List<Error>();
        foreach (var result in results)
        {
            if (!result.IsSuccess)
            {
                combinedErrors.Add(result.Error);
            }
        }
        return new Result(combinedErrors.Count == 0, Error.NotFoundError);
    }

    public static implicit operator Result(bool success)
    {
        return success ? Success() : Failure(new Error(0, "Operation failed."));
    }
}

public class Result<T> : Result
{
    public T Value { get; }
    public string ErrorMessage { get; private set; } = null!;

    protected Result(bool isSuccess, T value, Error error) : base(isSuccess, error)
    {
        Value = value;
    }

    public static new Result<T> Success(T value)
    {
        return new Result<T>(true, value, Error.NotFoundError);
    }

    public static new Result<T> Failure(Error error)
    {
        return new Result<T>(false, default, error);
    }

     

    public static new Result<T> Combine(params Result<T>[] results)
    {
        var combinedErrors = new List<Error>();
        T value = default;
        foreach (var result in results)
        {
            if (!result.IsSuccess)
            {
                combinedErrors.Add(result.Error);
            }
            else if (result.IsSuccess && value == null)
            {
                value = result.Value;
            }
        }
        return new Result<T>(combinedErrors.Count == 0, value, Error.NotFoundError);
    }
    public static implicit operator Result<T>(T value)
    {
        return Success(value);
    }
}