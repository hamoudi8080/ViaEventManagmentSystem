using UnitTests.Common.Factories.EventFactory;
using UnitTests.Fakes;
using ViaEventManagementSystem.Core.AppEntry.Commands.Event;
using ViaEventManagementSystem.Core.Application.CommandHandlers.Features.Event;
using ViaEventManagementSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagementSystem.Core.Domain.Common.UnitOfWork;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

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
        await eventRepo.AddAsync(viaEvent);

        Result<MakeEventPublicCommand> command =
            MakeEventPublicCommand.Create(viaEvent._eventId.Value.ToString()).Payload;

        var handler = new MakeEventPublicHandler(eventRepo, _unitOfWork);

        // Act
        var result = await handler.Handle(command.Payload);


        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(EventVisibility.Public, viaEvent._EventVisibility);
    }
}