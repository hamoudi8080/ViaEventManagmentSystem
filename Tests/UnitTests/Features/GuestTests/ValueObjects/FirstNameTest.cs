using ViaEventManagementSystem.Core.Tools.OperationResult;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace UnitTests.Features.GuestTests.ValueObjects;

public class FirstNameTest
{
    [Theory]
    [InlineData("John")]  // Valid first name
    [InlineData("Alice")] // Another valid first name
    public void Create_ValidFirstNames_Success(string firstName)
    {
        // Act
        var result = FirstName.Create(firstName);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(firstName, result.Payload.Value);
    }

    [Theory]

    [InlineData("")]      // Empty first name
    [InlineData("A")]     // Too short first name
    [InlineData("WayTooLongFirstNameThatExceedsTheMaxLength")] // Too long first name
    public void Create_InvalidFirstNames_Failure(string firstName)
    {
        // Act
        var result = FirstName.Create(firstName);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(Error.BadRequest(ErrorMessage.PersonName.FirstNameMustBeBetween2And25CharsOrIsNullOrWhiteSpace), result.Error);
    }

 
}
