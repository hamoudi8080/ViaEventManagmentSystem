using UnitTests.Common.Factories.EventFactory;
using UnitTests.Fakes;
using ViaEventManagmentSystem.Core.AppEntry.Commands.Event;
using ViaEventManagmentSystem.Core.Application.CommandHandlers.Features.Event;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagmentSystem.Core.Domain.Common.UnitOfWork;

namespace UnitTests.Features.Event.ReadiesEventTest;

public class MakeEventReadyHandlerTest
{
    private IUnitOfWork _unitOfWork = new UnitOfWork();
    
    [Fact]
    public async Task GivenDraftEvent_WithValidData_WhenMakeEventReady_ThenEventIsMadeReady()
    {
        // Arrange
        var viaEvent = ViaEventTestFactory.DraftEvent();
        
        var eventRepo = new EventRepository();
        eventRepo.Add(viaEvent);
        
        Result<MakeEventReadyCommand> command =
            MakeEventReadyCommand.Create(viaEvent._eventId.Value.ToString()).Payload;
        
        var handler = new MakeEventReadyHandler(eventRepo, _unitOfWork);

        // Act
        Result result = await handler.Handle(command.Payload);

        // Assert
        Assert.True(result.IsSuccess); // Check if the result is successful
        Assert.Equal(EventStatus.Ready, viaEvent._EventStatus);
      
    }
}