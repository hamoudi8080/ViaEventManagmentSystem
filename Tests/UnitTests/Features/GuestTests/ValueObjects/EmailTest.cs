using ViaEventManagementSystem.Core.Domain.Aggregates.Guests.ValueObjects;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace UnitTests.Features.GuestTests.ValueObjects;

public class EmailTest
{
    [Theory]

    [InlineData("test@via.dk", true)]
    [InlineData("123456@via.dk", true)]
    [InlineData("test123@via.com", false)] // Wrong domain
    [InlineData("test@via.com", false)] // Wrong domain
    [InlineData("123456@via.com", false)] // Wrong domain
    [InlineData("test1234@via.dk", false)] // Invalid text1 format
    [InlineData("te@via.dk", false)] // Text1 length too short
    [InlineData("test12345@via.dk", false)] // Text1 length too long
    public void Create_ValidEmail_ReturnsSuccess(string email, bool expectedResult)
    {
        // Act
        var result = Email.Create(email);

        // Assert
        Assert.Equal(expectedResult, result.IsSuccess);
    }

    [Theory]
    [InlineData("invalidemail")]
 
    public void Create_InvalidEmail_ReturnsFailure(string email)
    {
        // Act
        var result = Email.Create(email);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(ErrorMessage.TextFormatInvalid.ToString(), result.Error.Messages[0].ToString());
    }
    
    
    [Theory]
    [InlineData("invalid@via.com")]
    public void Create_InvalidEmail_EmailMustEndWithViaDK_ReturnsFailure(string email)
    {
        // Act
        var result = Email.Create(email);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(ErrorMessage.EmailMustEndWithViaDK.ToString(), result.Error.Messages[0].ToString());
    }
    
    [Theory]
    [InlineData("test12345@via.dk")]
    public void Create_InvalidEmail_ReturnsFailureTextLengthOutOfRange(string email)
    {
        // Act
        var result = Email.Create(email);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(ErrorMessage.Email.TextLengthOutOfRange.ToString(), result.Error.Messages[0].ToString());
    }
}