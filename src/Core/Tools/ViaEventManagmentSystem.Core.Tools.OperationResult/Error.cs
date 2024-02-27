namespace ViaEventManagmentSystem.Core.Tools.OperationResult;


public class Error
{
    public int ErrorCode { get; }
    public string ErrorMessage { get; }

    public Error(int errorCode, string errorMessage)
    {
        ErrorCode = errorCode;
        ErrorMessage = errorMessage;
    }

    public static Error NotFoundError => new Error((int)ErrorConstant.NotFound, "Not Found");
    public static Error ValidationError(string fieldName) => new Error(400, $"Validation error for {fieldName}");

  

    public static Error GetInvalidInputError()
    {
        return CreateError(ErrorConstant.InvalidInput, "Invalid input provided.");
    }

    public static Error GetUnauthorizedAccessError()
    {
        return CreateError(ErrorConstant.UnauthorizedAccess, "Unauthorized access.");
    }

    public static Error GetServerError()
    {
        return CreateError(ErrorConstant.ServerError, "Internal server error.");
    }

    public static Error GetNotFoundError()
    {
        return CreateError(ErrorConstant.NotFound, "Resource not found.");
    }

    public static Error GetTimeoutError()
    {
        return CreateError(ErrorConstant.Timeout, "Request timeout.");
    }

    private static Error CreateError(ErrorConstant errorCode, string errorMessage)
    {
        return new Error((int)errorCode, errorMessage);
    }
}