﻿using UnitTests.Common.Factories.EventFactory;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagmentSystem.Core.Tools.OperationResult;
using Xunit;

namespace UnitTests.Features.Event.UpdateTitleTest
{
    public abstract class UpdateTitleTest
    {
        public class S1
        {
            [Theory]
            [InlineData("Scary Movie Night!")]
            [InlineData("Graduation Gala")]
            [InlineData("VIA Hackathon")]
            public void UpdateTitle_Success_DraftStatus(string newTitle)
            {
                // Arrange
                var viaEvent = ViaEventTestFactory.CreateEvent();
                var eventTitle = EventTitle.Create(newTitle);

                // Act
                var result = viaEvent.UpdateTitle(eventTitle.Payload);

                // Assert
                Assert.True(result.IsSuccess);
                Assert.Equal(newTitle, viaEvent._EventTitle.Value);
                Assert.Equal(EventStatus.Draft, viaEvent._EventStatus);
                
            }
        }

        public class S2
        {
            [Theory]
            [InlineData("Scary Movie Night!")]
            [InlineData("Graduation Gala")]
            [InlineData("VIA Hackathon")]
            public void UpdateTitle_Success_ReadyStatus(string newTitle)
            {
                // Arrange
                var viaEvent = ViaEventTestFactory.ReadyEvent(); 
                var originalStatus = viaEvent._EventStatus;
                var eventTitle = EventTitle.Create(newTitle);

                // Act
                var result = viaEvent.UpdateTitle(eventTitle.Payload);

                // Assert
                Assert.True(result.IsSuccess);
                Assert.Equal(newTitle, viaEvent._EventTitle.Value); 
                Assert.Equal(EventStatus.Draft, viaEvent._EventStatus); 
                Assert.NotEqual(originalStatus, viaEvent._EventStatus); 
            }
        }

        public class F1
        {
            [Theory]
            [InlineData("")]
            public void UpdateTitle_Failure_EmptyTitle(string newTitle)
            {

                
                // Act
                Result<EventTitle> eventTitle = EventTitle.Create(newTitle);

                // Assert
                Assert.False(eventTitle.IsSuccess);
                Assert.Equal(ErrorMessage.TitleMustBeBetween3And75Chars.DisplayName, eventTitle.Error.Messages[0].ToString());
            }
        }

        
        public class F2
        {
            [Theory]
            [InlineData("XY")]
            [InlineData("A")]
            public void UpdateTitle_Failure_EmptyTitle(string newTitle)
            {
                
                // Act
                Result eventTitle = EventTitle.Create(newTitle);

                // Assert
                Assert.False(eventTitle.IsSuccess);
                Assert.Equal(ErrorMessage.TitleMustBeBetween3And75Chars.DisplayName, eventTitle.Error.Messages[0].ToString());
            }
        }

        public class F3
        {
            [Theory]
            [InlineData(
                "This is a very long title that exceeds the maximum character limit of 75 characters. This title is definitely too long.")]
            public void UpdateTitle_Failure_EmptyTitle(string newTitle)
            {


                // Act
                Result eventTitle = EventTitle.Create(newTitle);

                // Assert
                Assert.False(eventTitle.IsSuccess);
                Assert.Equal(ErrorMessage.TitleMustBeBetween3And75Chars.DisplayName, eventTitle.Error.Messages[0].ToString());
            }
        }

        public class F4
        {
            [Fact]
            public void UpdateTitle_Failure_NullTitle()
            {
    

                // Act
                Result eventTitle = EventTitle.Create(null);

                // Assert
                Assert.False(eventTitle.IsSuccess);
                Assert.Equal(ErrorMessage.TitleMustBeBetween3And75Chars.DisplayName, eventTitle.Error.Messages[0].ToString());
            }
        }

        public class F5
        {
            [Theory]
            [InlineData("Scary Movie Night!")]
            [InlineData("Graduation Gala")]
            [InlineData("VIA Hackathon")]
            public void UpdateTitle_Failure_ActiveEvent(string title)
            {
                // Arrange
                var viaEvent = ViaEventTestFactory.CreateActiveEvent();
                var eventTitle = EventTitle.Create(title);


                // Act
                var result = viaEvent.UpdateTitle(eventTitle.Payload);

                // Assert
                Assert.False(result.IsSuccess);

                Assert.Equal(ErrorMessage.ActiveEventCanotBeModified.DisplayName, result.Error.Messages[0].ToString());
            }
        }

        public class F6
        {
            [Theory]
            [InlineData("Scary Movie Night!")]
            [InlineData("Graduation Gala")]
            [InlineData("VIA Hackathon")]
            public void UpdateTitle_Failure_CancelledEvent(string title)
            {
                // Arrange
                var viaEvent = ViaEventTestFactory.CancelledEvent(); // Create a cancelled event
                var eventTitle = EventTitle.Create(title);


                // Act
                var result = viaEvent.UpdateTitle(eventTitle.Payload);

                // Assert
                Assert.False(result.IsSuccess); // Ensure the operation failed
                Assert.Equal(ErrorMessage.CancelledEventCannotBemodified.DisplayName,
                    result.Error.Messages[0].ToString()); // Check the error message
            }
        }
    }
}