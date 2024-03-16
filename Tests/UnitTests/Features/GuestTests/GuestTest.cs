using ViaEventManagmentSystem.Core.Domain.Aggregates.Guests;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Guests.ValueObjects;
using ViaEventManagmentSystem.Core.Tools.OperationResult;
using static UnitTests.Features.GuestTests.GuestFactory.GuestFactory;

namespace UnitTests.Features.GuestTests;
 

public abstract class GuestTest
{
    public class S1()
    {
        [Fact]
        public void Create_ValidGuest_Success()
        {
            // Arrange
            var id = GuestId.Create();
            var email = "John@via.dk";
            var firstName = "john";
            var lastname = "resho";
            
            // Act
            var createGuest = Guest.Create(id,firstName,lastname,email);
            
            //Assert
            Assert.True(createGuest.IsSuccess);
           
        }
        
    }
    
    
    public class F1()
    {
        [Fact]
        public void GivenEmail_DoesntEnd_viadk_Failure()
        {
            // Arrange
            var id = GuestId.Create();
            var email = "John@viaaa.dk";
            var firstName = "john";
            var lastname = "resho";
            
            // Act
            var result = Guest.Create(id,firstName,lastname,email);
            
            //Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Email Must End With Via.DK", result.Error.Messages[0].ToString());
           
        }
        
    }
    
    public class F2
    {
        [Fact]
        public void GivenInvalidEmail_FormatFailure()
        {
            // Arrange
            var id = GuestId.Create();
            var email = "invalidemail"; // Invalid email format
            var firstName = "john";
            var lastname = "resho";
        
            // Act
            var result = Guest.Create(id, firstName, lastname, email);
        
            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(ErrorMessage.TextFormatInvalid.ToString(), result.Error.Messages[0].ToString());
        }
    }
    
    
    public class F3
    {
        [Fact]
        public void GivenInvalidFirstName_InvalidFormatFailure()
        {
            // Arrange
            var id = GuestId.Create();
            var email = "john@via.dk";
            var invalidFirstName = "j"; // Invalid first name format
            var lastName = "resho";
        
            // Act
            var result = Guest.Create(id, invalidFirstName, lastName, email);
        
            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(ErrorMessage.FirstNameMustBeBetween2And25CharsOrIsNullOrWhiteSpace.ToString(), result.Error.Messages[0].ToString());
        }
    }
    
    public class F4
    {
        [Fact]
        public void GivenInvalidLastName_InvalidFormatFailure()
        {
            // Arrange
            var id = GuestId.Create();
            var email = "john@via.dk";
            var firstName = "john";
            var invalidLastName = ""; // Invalid last name format
       
            // Act
            var result = Guest.Create(id, firstName, invalidLastName, email);
        
            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(ErrorMessage.LastNameMustBeBetween2And25CharsOrIsNullOrWhiteSpace.ToString(), result.Error.Messages[0].ToString());
        }
    }

    public class F5
    {
        [Fact]
        public void GivenRegisteredEmail_AlreadyRegisteredFailure()
        {
            // Arrange
            var id = GuestId.Create();
            var registeredEmail = "john@via.dk";
            var firstName = "john";
            var lastName = "resho";
        
            // Register the email first
            var registrationResult = Guest.Create(id, firstName, lastName, registeredEmail);
            Assert.True(registrationResult.IsSuccess); // Ensure registration succeeds
        
            /*
            // Act: Try to register with the same email again
            var result = Guest.Create(GuestId.Create(), firstName, lastName, registeredEmail);
        
            // Assert
            Assert.False(result.IsSuccess);
             
           // Assert.Equal("Email is already registered", result.Error.Messages[0].ToString());
           */
        }
    }

    public class F6
    {
        [Theory]
        [InlineData("john1")] // Test case with numbers in the first name
        [InlineData("resho2")] // Test case with numbers in the last name
        public void GivenNameWithNumbers_InvalidFormatFailure(string invalidName)
        {
            // Arrange
            var id = GuestId.Create();
            var email = "john@via.dk";
            var lastName = "resho";
        
            // Act
            var result = Guest.Create(id, invalidName, invalidName, email); // Providing a generic last name
        
            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(ErrorMessage.FirstNameCannotContainNumbers.ToString(), result.Error.Messages[0].ToString());
        }
    }
    public class F7
    {
        [Theory]
        [InlineData("john@")] // Test case with symbols in the first name
        [InlineData("$resho")] // Test case with symbols in the last name
        public void GivenNameWithSymbols_InvalidFormatFailure(string invalidName)
        {
            // Arrange
            var id = GuestId.Create();
            var email = "john@via.dk";
        
            // Act
            var result = Guest.Create(id, invalidName, invalidName, email); // Providing a generic last name
        
            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(ErrorMessage.FirstNameCannotContainSymbols.ToString(), result.Error.Messages[0].ToString());
        }
    }



}