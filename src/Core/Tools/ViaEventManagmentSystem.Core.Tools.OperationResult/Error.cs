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
    public static Error NotFoundError => new Error(404, "Not Found");
    public static Error ValidationError(string fieldName) => new Error(400, $"Validation error for {fieldName}");
}

public static class ErrorConstants
{
    public static Error InvalidInput => new Error(1001, "Invalid input provided.");
    public static Error UnauthorizedAccess => new Error(1002, "Unauthorized access.");
    public static Error ServerError => new Error(500, "Internal server error.");
    public static Error NotFound => new Error(404, "Resource not found.");
    public static Error Timeout => new Error(408, "Request timeout.");
}