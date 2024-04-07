using UnitTests.Common.Factories.EventFactory;
using UnitTests.Fakes;
using ViaEventManagmentSystem.Core.AppEntry.Commands.Event;
using ViaEventManagmentSystem.Core.Application.CommandHandlers.Features.Event;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagmentSystem.Core.Domain.Common.UnitOfWork;

namespace UnitTests.Features.Event.ActivateEventTest;

public class ActivateEventHandlerTest
{
    private readonly IUnitOfWork _unitOfWork = new UnitOfWork();

    [Fact]
    public async Task ActivateEvent_WhenEventIsAnyOtherStatus_ShouldMakeEventActive_RetrunSuccess()
    {
        // Arrange
        var viaEvent = ViaEventTestFactory.ReadyEvent();
    
        var eventRepo = new EventRepository();
        eventRepo.Add(viaEvent);

        Result<ActivateEventCommand> command =
            ActivateEventCommand.Create(viaEvent._eventId.Value.ToString()).Payload;

        var handler = new ActivateEventHandler(eventRepo, _unitOfWork);

        // Act
        Result result = await handler.Handle(command.Payload);


        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(EventStatus.Active, viaEvent._EventStatus);
    }
}