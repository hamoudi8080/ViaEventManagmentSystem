using ViaEventManagementSystem.Core.Domain.Aggregates.Guests.ValueObjects;
using ViaEventManagementSystem.Core.Tools.OperationResult;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace UnitTests.Features.GuestTests.ValueObjects;

public class LastNameTest
{
    [Theory]
    [InlineData("Maxo")]  // Valid last name
    [InlineData("Resho")] // Another valid last name
    public void Create_ValidLastNames_Success(string lastName)
    {
        // Act
        var result = LastName.Create(lastName);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(lastName, result.Payload.Value);
    }

    [Theory]

    [InlineData("")]      // Empty last name
    [InlineData(null)]    // Null last name
    [InlineData("A")]     // Too short last name
    [InlineData("WayTooLongLastNameThatExceedsTheMaxLength")] // Too long first name
    public void Create_InvalidLastName_Failure(string lastname)
    {
        // Act
        var result = LastName.Create(lastname);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(Error.BadRequest(ErrorMessage.LastNameMustBeBetween2And25CharsOrIsNullOrWhiteSpace), result.Error);
    }
}