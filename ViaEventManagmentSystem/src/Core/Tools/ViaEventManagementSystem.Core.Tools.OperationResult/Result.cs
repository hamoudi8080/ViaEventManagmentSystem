namespace ViaEventManagmentSystem.Core.Tools.OperationResult;

public class Result
{
    protected Result(bool isSuccess)
    {
        IsSuccess = isSuccess;
    }

    public Result()
    {
    }

    protected Result(bool isSuccess, Error error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    protected Result(bool isSuccess, List<Error> error)
    {
        IsSuccess = isSuccess;
        ErrorCollection = error;
    }

    public bool IsSuccess { get; }
    public Error? Error { get; private init; }
    public string ErrorMessage => Error?.CustomMessage;
    public List<Error>? ErrorCollection { get; }

    public static Result Success()
    {
        return new Result(true);
    }

    public static Result Failure(Error error)
    {
        return new Result(false, error);
    }

    public static Result Failure(List<Error> errors)
    {
        return new Result(false, errors.ToList());
    }

    public static Result<T> CombineResultsInto<T>(params Result[] results)
    {
        var errors = new List<Error>();
        foreach (var r in results)
        {
            if (r.IsSuccess) continue;
            if (r.ErrorCollection != null) errors.AddRange(r.ErrorCollection);
            else if (r.Error != null) errors.Add(r.Error);
        }

        return errors.Count > 0 ? Result<T>.Failure(errors) : Result<T>.Success(default!);
    }

    public static implicit operator Result(Error error)
    {
        return new Result { Error = error };
    }
}

public class Result<T> : Result
{
    protected Result(bool isSuccess, T value, Error error) : base(isSuccess, error)
    {
        Payload = value;
    }

    protected Result(bool isSuccess, T value, List<Error> error) : base(isSuccess, error)
    {
        Payload = value;
    }

    private Result(bool isSuccess, T value) : base(isSuccess)
    {
        Payload = value;
    }

    public T Payload { get; private set; }


    public static Result<T> Success(T value)
    {
        return new Result<T>(true, value);
    }

    public new static Result<T> Failure(Error error)
    {
        return new Result<T>(false, default, error);
    }

    public new static Result<T> Failure(List<Error> errors)
    {
        return new Result<T>(false, default, errors.ToList());
    }

/*    public static Result<T> CombineFromOthers(params Result[] results)
    {
        var errors = new List<Error>();
        foreach (var result in results)
        {
            if (!result.IsSuccess)
            {
                if (result.ErrorCollection != null)
                {
                    errors.AddRange(result.ErrorCollection);
                }
                else if (result.Error != null)
                {
                    errors.Add(result.Error);
                }
            }
        }
        return errors.Any() ? Result<T>.Failure(errors) : Result<T>.Success(default);
    }
    */

    public Result<T> WithPayloadIfSuccess(Func<T> payloadFactory)
    {
        if (IsSuccess) return Success(payloadFactory());
        return ErrorCollection != null ? Failure(ErrorCollection) : Failure(Error!);
    }

    public static implicit operator Result<T>(T value)
    {
        return Success(value);
    }
}