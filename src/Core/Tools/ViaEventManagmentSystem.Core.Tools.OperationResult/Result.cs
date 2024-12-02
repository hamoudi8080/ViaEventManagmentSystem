using ViaEventManagmentSystem.Core.Tools.OperationResult;

public class Result
{
    public bool IsSuccess { get; }
    public Error? Error { get; private init; }

    public string ErrorMessage => Error?.CustomMessage; 
    public List<Error>? ErrorCollection { get; private init; }

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
  
    public static Result CombineFromOthers<T>(params Result[] results)
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
    public static implicit operator Result(Error error) => new Result { Error = error };
}
 
public class Result<T> : Result
{
    public T Payload { get; private set; }
   

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

   
    public static Result<T> Success(T value)
    {
        return new Result<T>(true, value); // Corrected to pass true for isSuccess
    }

    public new static Result<T> Failure(Error error)
    {
        return new Result<T>(false, default, error);
    }

    public new static Result<T> Failure(List<Error> errors)
    {
        return new Result<T>(false, default, errors.ToList());
    }

    public static Result<T> CombineFromOthers(params Result[] results)
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

    public static Result<T> WithPayloadIfSuccess(Result result, Func<T> payloadFactory)
    {
        if (result.IsSuccess)
        {
            return Result<T>.Success(payloadFactory());
        }
        return Result<T>.Failure(result.ErrorCollection);
    }
    public static implicit operator Result<T>(T value)
    {
        return Success(value);
    }
}