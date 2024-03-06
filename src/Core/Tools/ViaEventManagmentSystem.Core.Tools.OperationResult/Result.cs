using ViaEventManagmentSystem.Core.Tools.OperationResult;

public class Result
{
    public bool IsSuccess { get; }
    public Error? Error { get; init; }

    public string ErrorMessage => Error?.CustomMessage; 

    protected Result(bool isSuccess)
    {
        IsSuccess = isSuccess;
    }

    protected Result()
    {
    }

    protected Result(bool isSuccess, Error error)
    {
         
        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Success()
    {
        return new Result(true);
    }
 
    public static Result Failure(Error error)
    {
        return new Result(false, error);
    }

    public static implicit operator Result(Error error) => new Result { Error = error };
}

public class Result<T> : Result
{
    public T Payload { get; }
   

    protected Result(bool isSuccess, T value, Error error) : base(isSuccess, error)
    {
        Payload = value;
    }

    private Result() : base()
    {
    }

    private Result(bool isSuccess, T value) : base(isSuccess)
    {
        Payload = value;
    }

   
    public static new Result<T> Success(T value)
    {
        return new Result<T>(true, value); // Corrected to pass true for isSuccess
    }

    public static new Result<T> Failure(Error error)
    {
        return new Result<T>(false, default, error);
    }


   

    public static implicit operator Result<T>(T value)
    {
        return Success(value);
    }
}