using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace UnitTests.OperationResult;


public class OperationResulUnitTests
{
    [Fact]
    public void TestResultSuccess()
    {
        // Arrange

        // Act
        var result = Result.Success();

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public void TestResultFailureWithSingleError()
    {
        // Arrange
        var error = Error.NotFoundError;

        // Act
        var result = Result.Failure(error);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Single(result.Errors);
        Assert.Contains(error, result.Errors);
    }
/*
    [Fact]
    public void TestResultFailureWithMultipleErrors()
    {
        // Arrange
        var errors = new List<Error> { Error.InvalidInput, ErrorConstants.ServerError };

        // Act
        var result = Result.Failure(errors);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(2, result.Errors.Count);
        Assert.Contains(ErrorConstants.InvalidInput, result.Errors);
        Assert.Contains(ErrorConstants.ServerError, result.Errors);
    }

   
*/
    [Fact]
    public void TestErrorCreation()
    {
        // Arrange
        var errorCode = 404;
        var errorMessage = "Not Found";

        // Act
        var error = new Error(errorCode, errorMessage);

        // Assert
        Assert.Equal(errorCode, error.ErrorCode);
        Assert.Equal(errorMessage, error.ErrorMessage);
    }

    
}

