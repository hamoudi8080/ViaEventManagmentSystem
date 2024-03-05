namespace ViaEventManagmentSystem.Core.Tools.OperationResult
{
    public class ErrorConstant : Enumeration
    {
        private ErrorConstant() { }

        private ErrorConstant(int value, string displayName) : base(value, displayName) { }

        public static readonly ErrorConstant Unknown =
            new ErrorConstant(0, "Unknown");
      

        public static readonly ErrorConstant BadRequest =
            new ErrorConstant(400, "Bad Request");

        public static readonly ErrorConstant Unauthorized =
            new ErrorConstant(401, "Unauthorized");

        public static readonly ErrorConstant Forbidden =
            new ErrorConstant(403, "Forbidden");

        public static readonly ErrorConstant NotFound =
            new ErrorConstant(404, "Not Found");

        public static readonly ErrorConstant MethodNotAllowed =
            new ErrorConstant(405, "Method Not Allowed");

        public static readonly ErrorConstant Conflict =
            new ErrorConstant(409, "Conflict");

        public static readonly ErrorConstant PreconditionFailed =
            new ErrorConstant(412, "Precondition Failed");

        public static readonly ErrorConstant PayloadTooLarge =
            new ErrorConstant(413, "Payload Too Large");

        public static readonly ErrorConstant UnsupportedMediaType =
            new ErrorConstant(415, "Unsupported Media Type");

        public static readonly ErrorConstant UnprocessableEntity =
            new ErrorConstant(422, "Unprocessable Entity");

        public static readonly ErrorConstant TooManyRequests =
            new ErrorConstant(429, "Too Many Requests");

        public static readonly ErrorConstant InternalServerError =
            new ErrorConstant(500, "Internal Server Error");

        public static readonly ErrorConstant NotImplemented =
            new ErrorConstant(501, "Not Implemented");

        public static readonly ErrorConstant BadGateway =
            new ErrorConstant(502, "Bad Gateway");

        public static readonly ErrorConstant ServiceUnavailable =
            new ErrorConstant(503, "Service Unavailable");

        public static readonly ErrorConstant GatewayTimeout =
            new ErrorConstant(504, "Gateway Timeout");
    }
}
