using UnitTests.Common.Factories.EventFactory;
using UnitTests.Fakes;
using ViaEventManagmentSystem.Core.AppEntry.Commands;
using ViaEventManagmentSystem.Core.AppEntry.Commands.Event;
using ViaEventManagmentSystem.Core.Application.CommandHandlers.Features.Event;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagmentSystem.Core.Domain.Common.UnitOfWork;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace UnitTests.Features.Event.UpdateTitleTest;

public class UpdateEventTitleHandlerTest
{
    private readonly IUnitOfWork _unitOfWork = new UnitOfWork();

    [Fact]
    public async Task UpdateEventTitleHandler_Success()
    {
        // Arrange

        //Todo: ask why in the root of viaEvent Id is always null??   and how do i fix it 
        var viaEvent = ViaEventTestFactory.ReadyEvent();
        var eventRepo = new EventRepository();
        eventRepo.Add(viaEvent);

        var title = "New Title";
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