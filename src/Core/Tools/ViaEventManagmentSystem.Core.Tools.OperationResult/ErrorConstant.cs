namespace ViaEventManagmentSystem.Core.Tools.OperationResult;

public enum ErrorConstant
{
    
    InvalidInput = 1001,
    Unknown = 0,
    BadRequest = 400,
    UnauthorizedAccess = 1002,
    ServerError = 500,
    Conflict = 409,
    NotFound = 404,
    Timeout = 408
}