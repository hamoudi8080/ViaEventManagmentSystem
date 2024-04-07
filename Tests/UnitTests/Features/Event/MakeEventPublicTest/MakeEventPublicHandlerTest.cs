using UnitTests.Common.Factories.EventFactory;
using UnitTests.Fakes;
using ViaEventManagmentSystem.Core.AppEntry.Commands.Event;
using ViaEventManagmentSystem.Core.Application.CommandHandlers.Features.Event;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagmentSystem.Core.Domain.Common.UnitOfWork;

namespace UnitTests.Features.Event.MakeEventPublicTest;

public class MakeEventPublicHandlerTest
{
    private readonly IUnitOfWork _unitOfWork = new UnitOfWork();

    [Fact]
    public async Task MakeEventPublicHandler_WhenEventIsNotPublic_ShouldMakeEventPublic()
    {
        // Arrange
        var viaEvent = ViaEventTestFactory.PrivateEvent();

        var eventRepo = new EventRepository();
        eventRepo.Add(viaEvent);

        Result<MakeEventPublicCommand> command =
            MakeEventPublicCommand.Create(viaEvent._eventId.Value.ToString()).Payload;

        var handler = new MakeEventPublicHandler(eventRepo, _unitOfWork);

        // Act
        Result result = await handler.Handle(command.Payload);


        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(EventVisibility.Public, viaEvent._EventVisibility);
    }

 
}