using UnitTests.Common.Factories.EventFactory;
using UnitTests.Fakes;
using ViaEventManagmentSystem.Core.AppEntry.Commands.Event;
using ViaEventManagmentSystem.Core.Application.CommandHandlers.Features.Event;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagmentSystem.Core.Domain.Common.UnitOfWork;

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
        eventRepo.Add(viaEvent);

        Result<MakeEventPrivateCommand> command =
            MakeEventPrivateCommand.Create(viaEvent._eventId.Value.ToString()).Payload;

        var handler = new MakeEventPrivateHandler(eventRepo, _unitOfWork);

        // Act
        Result result = await handler.Handle(command.Payload);


        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(EventVisibility.Private, viaEvent._EventVisibility);
    }
}