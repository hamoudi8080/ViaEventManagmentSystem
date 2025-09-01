using UnitTests.Common.Factories.EventFactory;
using ViaEventManagementSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagementSystem.Core.Tools.OperationResult;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace UnitTests.Features.Event.SetMaxNumberOfGuestsTest;

public abstract class SetMaxNumberOfGuestsTest
{
    public class S1
    {
        [Theory]
        [InlineData(5)]
        [InlineData(10)]
        [InlineData(25)]
        [InlineData(50)]
        public void SetMaxNumberOfGuests_ExistingEventWithDraftStatusAndValidNumberOfGuests_Success(int numberOfGuests)
        {
            // Arrange

            var viaEvent = ViaEventTestFactory.CreateEvent();
            var maxNoOfGuest = MaxNumberOfGuests.Create(numberOfGuests);

            // Act
            var result = viaEvent.SetMaxNumberOfGuests(maxNoOfGuest.Payload);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(numberOfGuests, result.Payload._MaxNumberOfGuests.Value); 
        }
    }

    public class S2
    {
        [Theory]
        [InlineData(5)]
        [InlineData(10)]
        [InlineData(25)]
        [InlineData(50)]
        public void SetMaxNumberOfGuests_ExistingEventWithDraftOrReadyStatusAndValidNumberOfGuests_Success(
            int numberOfGuests)
        {
            // Arrange
            var viaEvent = ViaEventTestFactory.ReadyEvent();
            var maxNoOfGuest = MaxNumberOfGuests.Create(numberOfGuests);

            // Act
            var result = viaEvent.SetMaxNumberOfGuests(maxNoOfGuest.Payload);

            // Assert
            Assert.True(result.IsSuccess); 
            Assert.Equal(numberOfGuests,result.Payload._MaxNumberOfGuests!.Value); 
        }
    }


    public class S3
    {
        [Theory]
        [InlineData(5)]
        [InlineData(25)]
        [InlineData(50)]
        public void SetMaxNumberOfGuests_ExistingEventWithActiveStatusAndValidNumberOfGuests_Success(int numberOfGuests)
        {
            // Arrange
            var viaEvent = ViaEventTestFactory.CreateActiveEvent(); 
            var newMaxNumberOfGuests = MaxNumberOfGuests.Create(numberOfGuests); 
            // Act
            var result = viaEvent.SetMaxNumberOfGuests(newMaxNumberOfGuests.Payload);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(numberOfGuests, result.Payload._MaxNumberOfGuests.Value);  
    
        }
    }

    public class F1
    {
        [Fact]
        public void SetMaxNumberOfGuests_ReduceNumberOfMaximumGuestsInActiveEvent_Failure()
        {
            // Arrange
            var viaEvent = ViaEventTestFactory.CreateActiveEvent();
            var setMaxNumberOfGuests = MaxNumberOfGuests.Create(10);
            viaEvent.SetMaxNumberOfGuests(setMaxNumberOfGuests.Payload);
            
            var currentMaxNumberOfGuests = viaEvent._MaxNumberOfGuests.Value; 
            
            var newMaxNumberOfGuests =
                MaxNumberOfGuests.Create(currentMaxNumberOfGuests - 1); 

            // Act
            var result = viaEvent.SetMaxNumberOfGuests(newMaxNumberOfGuests.Payload);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(ErrorMessage.ActiveEventCannotReduceMaxGuests.DisplayName, result.Error.Messages[0].DisplayName);  
        }
    }

    public class F2
    {
        [Fact]
        public void SetMaxNumberOfGuests_CancelledEvent_Failure()
        {
            // Arrange
            var viaEvent = ViaEventTestFactory.CancelledEvent(); 
            var newMaxNumberOfGuests = MaxNumberOfGuests.Create(10); 

            // Act
            var result = viaEvent.SetMaxNumberOfGuests(newMaxNumberOfGuests.Payload);

            // Assert
            Assert.False(result.IsSuccess); // Check if the operation failed
            Assert.Equal(ErrorMessage.CancelledEventCannotBemodified.DisplayName, result.Error.Messages[0].DisplayName);
        }
    }


    public class F4
    {
        [Fact]
        public void SetMaxNumberOfGuests_LessThan5_Failure()
        {
            // Arrange
            var viaEvent = ViaEventTestFactory.CreateEvent();
         

            // Act
            Result result = MaxNumberOfGuests.Create(4); // Set the new maximum number of guests less than 5

            // Assert
            Assert.False(result.IsSuccess); // Check if the operation failed
            Assert.Equal(ErrorMessage.MaxGuestsNoMustBeWithin5and50.DisplayName,
                result.Error.Messages[0].ToString()); // Check if the failure message is correct
        }
    }

    public class F5
    {
        [Fact]
        public void SetMaxNumberOfGuests_MoreThan50_Failure()
        {
         
            // Act
            Result result = MaxNumberOfGuests.Create(4); 
            
            // Assert
            Assert.False(result.IsSuccess); 
            Assert.Equal(ErrorMessage.MaxGuestsNoMustBeWithin5and50.DisplayName,
                result.Error.Messages[0].ToString()); 
        }
    }
}