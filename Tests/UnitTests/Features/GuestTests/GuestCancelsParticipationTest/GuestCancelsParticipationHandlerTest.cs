using UnitTests.Common.Factories.EventFactory;
using UnitTests.Common.Factories.GuestFactory;
using UnitTests.Fakes;
using ViaEventManagmentSystem.Core.AppEntry.Commands.Event;
using ViaEventManagmentSystem.Core.Application.CommandHandlers.Features.Event;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.Entities.ValueObjects;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagmentSystem.Core.Domain.Common.UnitOfWork;

namespace UnitTests.Features.GuestTests.GuestCancelsParticipationTest;

public class GuestCancelsParticipationHandlerTest
{
    private readonly IUnitOfWork _unitOfWork = new UnitOfWork();
    [Fact]
    public async void GivenEventIdAndGuestId_GuestCancelsParticipationCommandIsCreated()
    {

        // Arrange
        var viaEvent = ViaEventTestFactory.ReadyEvent();
        viaEvent.ActivateEvent();
        var futureevent = DateTime.Now.AddMinutes(30);
        var s = StartDateTime.Create(futureevent);
        viaEvent.AddEventStartTime(s.Payload);

        
        var guest = GuestFactory.CreateGuest();
         viaEvent.InviteGuest(guest._Id);
         viaEvent.AcceptGuestInvitation(guest._Id);
        
        var eventRepo = new EventRepository ();
        eventRepo.Add(viaEvent);
        
        GuestCancelsParticipationCommand guestCancels = GuestCancelsParticipationCommand.Create(viaEvent._eventId.Value.ToString(), guest._Id.Value.ToString()).Payload;
        GuestCancelsParticipationHandler handler = new(eventRepo, _unitOfWork);
         
        // Act
        var result = await handler.Handle(guestCancels);
        
        
        // Assert
        Assert.True(result.IsSuccess);
        Assert.False(viaEvent._GuestsParticipants.Any(x => x.Value == guest._Id.Value));
    }
}