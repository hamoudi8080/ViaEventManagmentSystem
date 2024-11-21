using ViaEventManagmentSystem.Core.Domain.Aggregates.Guests;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Guests.ValueObjects;
using ViaEventManagmentSystem.Core.Tools.OperationResult;
using static UnitTests.Common.Factories.GuestFactory.GuestFactory;

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
            var firstName = FirstName.Create("John").Payload;
            var lastname = LastName.Create("Resho").Payload;
            var email = Email.Create("John@via.dk").Payload;
            
            // Act
            var createGuest = Guest.Create(id.Payload,firstName,lastname,email);
            
            //Assert
            Assert.True(createGuest.IsSuccess);
           
        }
        
    }
    
    
    public class F1()
    {
        [Fact]
        public void GivenEmail_DoesntEnd_viadk_Failure()
        {
            // Act
            var email = Email.Create("John@via.com");
            
            //Assert
            Assert.False(email.IsSuccess);
            Assert.Equal(ErrorMessage.EmailMustEndWithViaDK.DisplayName, email.Error.Messages[0].DisplayName);
           
        }
        
    }
    
    public class F2
    {
        [Fact]
        public void GivenInvalidEmail_FormatFailure()
        {

            // Act
            var email = Email.Create("John");
        
            // Assert
            Assert.False(email.IsSuccess);
            Assert.Equal(ErrorMessage.TextFormatInvalid.ToString(), email.Error.Messages[0].ToString());
        }
    }
    
    
    public class F3
    {
        [Fact]
        public void GivenInvalidFirstName_InvalidFormatFailure()
        {
            
            // Act
            var firstName = FirstName.Create("j");
        
            // Assert
            Assert.False(firstName.IsSuccess);
            Assert.Equal(ErrorMessage.FirstNameMustBeBetween2And25CharsOrIsNullOrWhiteSpace.ToString(), firstName.Error.Messages[0].ToString());
        }
    }
    

    
    public class F4
    {
        [Fact]
        public void GivenInvalidLastName_InvalidFormatFailure()
        {


            // Act
            var lastname = LastName.Create("j");

            // Assert
            Assert.False(lastname.IsSuccess);
            Assert.Equal(ErrorMessage.LastNameMustBeBetween2And25CharsOrIsNullOrWhiteSpace.ToString(), lastname.Error.Messages[0].ToString());
        }
    }

    /*
    public class F5
    {
        [Fact]
        public void GivenRegisteredEmail_AlreadyRegisteredFailure()
        {
            // Arrange
            var id = GuestId.Create();
            var firstName = FirstName.Create("John").Payload;
            var lastname = LastName.Create("Resho").Payload;
            var email = Email.Create("John@via.dk").Payload;
            
            
            // Register the email first
            var registrationResult = Guest.Create(id.Payload, firstName, lastname, email);
            Assert.True(registrationResult.IsSuccess); // Ensure registration succeeds

           
            // Act: Try to register with the same email again
            var result = Guest.Create(GuestId.Create().Payload, firstName, lastname, email);

            // Assert
            Assert.False(result.IsSuccess);

           // Assert.Equal("Email is already registered", result.Error.Messages[0].ToString());
   
        }
    }
*/


    public class F6
    {
        [Theory]
        [InlineData("john1")]  
        [InlineData("resho2")]  
        public void GivenNameWithNumbers_InvalidFormatFailure(string invalidName)
        {
 
            // Act
            var id = GuestId.Create();
            var firstName = FirstName.Create(invalidName);
            var lastname = LastName.Create(invalidName);

        
            // Assert
            Assert.False(firstName.IsSuccess);
            Assert.Equal(ErrorMessage.FirstNameCannotContainNumbers.ToString(), firstName.Error.Messages[0].ToString());
        }
    }
    

    public class F7
    {
        [Theory]
        [InlineData("john@")] // Test case with symbols in the first name
        [InlineData("$resho")] // Test case with symbols in the last name
        public void GivenNameWithSymbols_InvalidFormatFailure(string invalidName)
        {
            
            // Act
            var firstName = FirstName.Create(invalidName);
            var lastname = LastName.Create(invalidName);

        
            // Assert
            Assert.False(lastname.IsSuccess);
            Assert.Equal(ErrorMessage.LastNameCannotContainSymbols.ToString(), lastname.Error.Messages[0].ToString());
        }
    }




}