using UnitTests.Common.Factories.EventFactory;
using UnitTests.Common.Factories.GuestFactory;
using UnitTests.Fakes;
using ViaEventManagementSystem.Core.AppEntry.Commands.Event;
using ViaEventManagementSystem.Core.Application.CommandHandlers.Features.Event;
using ViaEventManagementSystem.Core.Domain.Aggregates.Events.Entities.ValueObjects;
using ViaEventManagementSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagementSystem.Core.Domain.Common.UnitOfWork;

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
         viaEvent.InviteGuest(guest.Id);
         viaEvent.AcceptGuestInvitation(guest.Id);
        
        var eventRepo = new EventRepository ();
        eventRepo.Add(viaEvent);
        
        GuestCancelsParticipationCommand guestCancels = GuestCancelsParticipationCommand.Create(viaEvent._eventId.Value.ToString(), guest.Id.Value.ToString()).Payload;
        GuestCancelsParticipationHandler handler = new(eventRepo, _unitOfWork);
         
        // Act
        var result = await handler.Handle(guestCancels);
        
        
        // Assert
        Assert.True(result.IsSuccess);
        //Assert.False(viaEvent._GuestsParticipants.Any(x => x.GuestId.Value == guest.Id.Value));
    }
}