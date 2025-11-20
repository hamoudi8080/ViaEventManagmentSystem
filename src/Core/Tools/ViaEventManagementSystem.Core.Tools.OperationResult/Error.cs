using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagementSystem.Core.Tools.OperationResult;


public class Error
{
    public ErrorConstant  ErrorCode { get; }
    public List<ErrorMessage> Messages { get; init; }
    public string CustomMessage { get; init; }
    

    internal Error(ErrorConstant errorCode, List<ErrorMessage> messages) {
        ErrorCode = errorCode;
        
        if (messages.Count == 0) {
            throw new Exception(" one error message is required");
        }
        Messages = messages;
    }
    internal Error(string message) {
        
        CustomMessage = message;
    }
    internal Error(ErrorConstant errorCode, ErrorMessage message) {
        ErrorCode = errorCode;
        Messages = new List<ErrorMessage> {message};
    }

    public static Error NotFound(ErrorMessage message)
    {
        return new Error(ErrorConstant.NotFound, message);
    }

    public static Error BadRequest(ErrorMessage message ) {
        return new Error(ErrorConstant.BadRequest, message);
    } 
    

    public static Error AddCustomError(string InvalidInputError)
    {
        return CreateError(InvalidInputError);
    }


    private static Error CreateError(string errorMessage)
    {
        return new Error(errorMessage);
    }
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        Error other = (Error)obj;
        return ErrorCode == other.ErrorCode && Messages.SequenceEqual(other.Messages);
    }
}