using UnitTests.Common.Factories.EventFactory;
using UnitTests.Fakes;
using ViaEventManagementSystem.Core.AppEntry.Commands.Event;
using ViaEventManagementSystem.Core.Application.CommandHandlers.Features.Event;
using ViaEventManagementSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagementSystem.Core.Domain.Common.UnitOfWork;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace UnitTests.Features.Event.MakeEventPrivateTest;

public class MakeEventPrivateHandlerTest
{
    private readonly IUnitOfWork _unitOfWork = new UnitOfWork();

    [Fact]
    public async Task MakeEventPrivateHandler_WhenEventIsNotPrivate_ShouldMakeEventPrivate()
    {
        // Arrange
        var viaEvent = ViaEventTestFactory.ReadyEvent();

        var eventRepo = new EventRepository();
        await eventRepo.AddAsync(viaEvent);

        Result<MakeEventPrivateCommand> command =
            MakeEventPrivateCommand.Create(viaEvent._eventId.Value.ToString()).Payload;

        var handler = new MakeEventPrivateHandler(eventRepo, _unitOfWork);

        // Act
        var result = await handler.Handle(command.Payload);


        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(EventVisibility.Private, viaEvent._EventVisibility);
    }
}