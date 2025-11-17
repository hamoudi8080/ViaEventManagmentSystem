using UnitTests.Common.Factories.EventFactory;
using UnitTests.Fakes;
using ViaEventManagementSystem.Core.AppEntry.Commands.Event;
using ViaEventManagementSystem.Core.Application.CommandHandlers.Features.Event;
using ViaEventManagementSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagementSystem.Core.Domain.Common.UnitOfWork;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace UnitTests.Features.Event.ReadiesEventTest;

public class MakeEventReadyHandlerTest
{
    private readonly IUnitOfWork _unitOfWork = new UnitOfWork();

    [Fact]
    public async Task GivenDraftEvent_WithValidData_WhenMakeEventReady_ThenEventIsMadeReady()
    {
        // Arrange
        var viaEvent = ViaEventTestFactory.DraftEvent();

        var eventRepo = new EventRepository();
        await eventRepo.AddAsync(viaEvent);

        Result<MakeEventReadyCommand> command =
            MakeEventReadyCommand.Create(viaEvent._eventId.Value.ToString()).Payload;

        var handler = new MakeEventReadyHandler(eventRepo, _unitOfWork);

        // Act
        var result = await handler.Handle(command.Payload);

        // Assert
        Assert.True(result.IsSuccess); // Check if the result is successful
        Assert.Equal(EventStatus.Ready, viaEvent._EventStatus);
    }
}