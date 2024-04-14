using UnitTests.Common.Factories.EventFactory;
using UnitTests.Common.Factories.GuestFactory;
using UnitTests.Fakes;
using ViaEventManagmentSystem.Core.AppEntry.Commands.Event;
using ViaEventManagmentSystem.Core.Application.CommandHandlers.Features.Event;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagmentSystem.Core.Domain.Common.UnitOfWork;

namespace UnitTests.Features.GuestTests.GuestParticipationTest;

public class ParticipateGuestHandlerTest
{
    private readonly IUnitOfWork _unitOfWork = new UnitOfWork();
    [Fact]
    public async Task CreateReadyEvent_AddGuestToParticipate_ReturnSuccess()
    {
        // Arrange
        var viaEvent = ViaEventTestFactory.ReadyEvent();
        viaEvent.ActivateEvent();
        var futureevent = DateTime.Now.AddMinutes(30);
        var s = StartDateTime.Create(futureevent);
        viaEvent.AddEventStartTime(s.Payload);
        
        var guest = GuestFactory.CreateGuest();
        var eventRepo = new EventRepository();
        eventRepo.Add(viaEvent);
        
        ParticipateGuestCommand participateGuestCommand = ParticipateGuestCommand.Create(viaEvent._eventId.Value.ToString(), guest.Id.Value.ToString()).Payload;
        ParticipateGuestHandler handler = new(eventRepo, _unitOfWork);
        
        // Act
        var result = await handler.Handle(participateGuestCommand);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.True(viaEvent._GuestsParticipants.Any(participation => participation.Value == guest.Id.Value));
    }
}