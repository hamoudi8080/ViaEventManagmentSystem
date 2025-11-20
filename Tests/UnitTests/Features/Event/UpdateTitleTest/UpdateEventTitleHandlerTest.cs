using UnitTests.Common.Factories.EventFactory;
using UnitTests.Fakes;
using ViaEventManagementSystem.Core.AppEntry.Commands;
using ViaEventManagementSystem.Core.AppEntry.Commands.Event;
using ViaEventManagementSystem.Core.Application.CommandHandlers.Features.Event;
using ViaEventManagementSystem.Core.Domain.Aggregates.Events;
using ViaEventManagementSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagementSystem.Core.Domain.Common.UnitOfWork;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace UnitTests.Features.Event.UpdateTitleTest;

public class UpdateEventTitleHandlerTest
{
    private readonly IUnitOfWork _unitOfWork = new UnitOfWork();

    [Fact]
    public async Task UpdateEventTitleHandler_Success()
    {
        // Arrange
        var viaEvent = ViaEventTestFactory.ReadyEvent();
        var eventRepo = new EventRepository();
        eventRepo.Add(viaEvent);
        var title = "Sports Event";
        var eventtitle = EventTitle.Create(title);

        UpdateEventTitleCommand updateEventTitleCommand = UpdateEventTitleCommand
            .Create(viaEvent._eventId.Value.ToString(), eventtitle.Payload.Value).Payload!;

        UpdateEventTitleHandler handler = new(eventRepo, _unitOfWork);

        // Act
        var result = await handler.Handle(updateEventTitleCommand);


        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(eventtitle.Payload.Value, viaEvent._EventTitle.Value);
        Assert.Equal(EventStatus.Draft, viaEvent._EventStatus);
        Assert.Single(eventRepo._Events);
    }
}