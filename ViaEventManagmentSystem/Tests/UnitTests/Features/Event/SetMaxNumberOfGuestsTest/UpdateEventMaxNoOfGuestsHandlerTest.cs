using UnitTests.Common.Factories.EventFactory;
using UnitTests.Fakes;
using ViaEventManagementSystem.Core.AppEntry.Commands.Event;
using ViaEventManagementSystem.Core.Application.CommandHandlers.Features.Event;
using ViaEventManagementSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagementSystem.Core.Domain.Common.UnitOfWork;

namespace UnitTests.Features.Event.SetMaxNumberOfGuestsTest;

public class UpdateEventMaxNoOfGuestsHandlerTest
{
    private readonly IUnitOfWork _unitOfWork = new UnitOfWork();

    [Fact]
    public async Task HandleUpdateEventMaxNoOfGuestsCommand_WithValidData_ShouldReturnSuccess()
    {
        // Arrange event and add to the list of events
        var viaEvent = ViaEventTestFactory.CreateActiveEvent();

        var eventRepo = new EventRepository();
        await eventRepo.AddAsync(viaEvent);

        var setMaxNoOfGuests = MaxNumberOfGuests.Create(5);
        viaEvent.SetMaxNumberOfGuests(setMaxNoOfGuests.Payload);

        var command = UpdateEventMaxNoOfGuestsCommand.Create(viaEvent._eventId.Value.ToString(), 10).Payload;
        var handler = new UpdateEventMaxNoOfGuestsHandler(eventRepo, _unitOfWork);

        // Act
        var result = await handler.Handle(command);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(10, viaEvent._MaxNumberOfGuests.Value);
    }
}