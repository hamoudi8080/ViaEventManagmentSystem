using UnitTests.Common.Factories.EventFactory;
using UnitTests.Common.Factories.GuestFactory;
using UnitTests.Fakes;
using ViaEventManagmentSystem.Core.AppEntry.Commands.Event;
using ViaEventManagmentSystem.Core.Application.CommandHandlers.Features.Event;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.Entities.ValueObjects;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagmentSystem.Core.Domain.Common.UnitOfWork;

namespace UnitTests.Features.GuestTests.GuestAcceptInvitationTest;

public class AcceptInvitationHandlerTest
{
    private readonly IUnitOfWork _unitOfWork = new UnitOfWork();
    
    [Fact]
    public async Task AcceptInvitationHandler_WhenGuestAcceptsInvitation_ShouldAcceptInvitationAndBeAddedToParticipationsList()
    {
        
       
        // Arrange
        var viaEvent = ViaEventTestFactory.ReadyEvent();
        viaEvent.ActivateEvent();
        var futureevent = DateTime.Now.AddMinutes(30);
        var s = StartDateTime.Create(futureevent);
        viaEvent.AddEventStartTime(s.Payload);

        
        var guest = GuestFactory.CreateGuest();
        var invitation = viaEvent.InviteGuest(guest.Id);
        
        var eventRepo = new EventRepository ();
         eventRepo.Add(viaEvent);
        
        AcceptInvitationCommand acceptInvitationCommand = AcceptInvitationCommand.Create(viaEvent._eventId.Value.ToString(), guest.Id.Value.ToString()).Payload;
        AcceptInvitationHandler handler = new(eventRepo, _unitOfWork);
         
        // Act
        var result = await handler.Handle(acceptInvitationCommand);
        
        
        // Assert
        Assert.True(result.IsSuccess);
        Assert.True(viaEvent._Invitations.Any(x => x._GuestId.Value == guest.Id.Value && x._InvitationStatus == InvitationStatus.Accepted));
        Assert.True(viaEvent._GuestsParticipants.Any(x => x.GuestId.Value == guest.Id.Value));



    }
}