using UnitTests.Common.Factories.EventFactory;
using UnitTests.Common.Factories.GuestFactory;
using UnitTests.Fakes;
using ViaEventManagementSystem.Core.AppEntry.Commands.Event;
using ViaEventManagementSystem.Core.Application.CommandHandlers.Features.Event;
using ViaEventManagementSystem.Core.Domain.Aggregates.Events.Entities.ValueObjects;
using ViaEventManagementSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagementSystem.Core.Domain.Common.UnitOfWork;

namespace UnitTests.Features.GuestTests.GuestRejectInvitationTest;

public class DeclineInvitationHandlerTest
{
    private IUnitOfWork _unitOfWork = new UnitOfWork();
    
    [Fact]
    public async Task Handle_WhenInvoke_GuestDeclinesTheRequest_ShouldReturnSuccess()
    {
      
        // Arrange
        var viaEvent = ViaEventTestFactory.ReadyEvent();
        viaEvent.ActivateEvent();
        var futureevent = DateTime.Now.AddMinutes(30);
        var s = StartDateTime.Create(futureevent);
        viaEvent.AddEventStartTime(s.Payload);
        var guest = GuestFactory.CreateGuest();
        
        var eventRepo = new EventRepository ();
        eventRepo.Add(viaEvent);
        
        DeclineInvitationCommand acceptInvitationCommand = DeclineInvitationCommand.Create(viaEvent._eventId.Value.ToString(), guest.Id.Value.ToString()).Payload;
        DeclineInvitationHandler handler = new(eventRepo, _unitOfWork);
         
        // Act
        var result = await handler.Handle(acceptInvitationCommand);
        
        
        // Assert
        Assert.True(result.IsSuccess);
        Assert.False(viaEvent._Invitations.Any(x => x._GuestId.Value == guest.Id.Value && x._InvitationStatus == InvitationStatus.Accepted));
     
    }
}