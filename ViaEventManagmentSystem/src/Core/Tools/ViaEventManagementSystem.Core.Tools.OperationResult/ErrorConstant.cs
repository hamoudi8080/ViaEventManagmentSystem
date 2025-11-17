namespace ViaEventManagmentSystem.Core.Tools.OperationResult;

public class ErrorConstant : Enumeration
{
    public static readonly ErrorConstant Unknown = new(0, "Unknown");


    public static readonly ErrorConstant BadRequest = new(400, "Bad Request");

    public static readonly ErrorConstant Unauthorized = new(401, "Unauthorized");

    public static readonly ErrorConstant Forbidden = new(403, "Forbidden");

    public static readonly ErrorConstant NotFound = new(404, "Not Found");

    public static readonly ErrorConstant MethodNotAllowed = new(405, "Method Not Allowed");

    public static readonly ErrorConstant Conflict = new(409, "Conflict");

    public static readonly ErrorConstant PreconditionFailed = new(412, "Precondition Failed");

    public static readonly ErrorConstant PayloadTooLarge = new(413, "Payload Too Large");

    public static readonly ErrorConstant UnsupportedMediaType = new(415, "Unsupported Media Type");

    public static readonly ErrorConstant UnprocessableEntity = new(422, "Unprocessable Entities");

    public static readonly ErrorConstant TooManyRequests = new(429, "Too Many Requests");

    public static readonly ErrorConstant InternalServerError = new(500, "Internal Server Error");

    public static readonly ErrorConstant NotImplemented = new(501, "Not Implemented");

    public static readonly ErrorConstant BadGateway = new(502, "Bad Gateway");

    public static readonly ErrorConstant ServiceUnavailable = new(503, "Service Unavailable");

    public static readonly ErrorConstant GatewayTimeout = new(504, "Gateway Timeout");

    private ErrorConstant()
    {
    }

    private ErrorConstant(int value, string displayName) : base(value, displayName)
    {
    }
}