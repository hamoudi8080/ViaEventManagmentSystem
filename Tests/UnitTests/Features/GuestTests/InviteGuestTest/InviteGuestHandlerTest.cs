using UnitTests.Common.Factories.EventFactory;
using UnitTests.Common.Factories.GuestFactory;
using UnitTests.Fakes;
using ViaEventManagmentSystem.Core.AppEntry.Commands.Event;
using ViaEventManagmentSystem.Core.Application.CommandHandlers.Features.Event;
using ViaEventManagmentSystem.Core.Domain.Common.UnitOfWork;

namespace UnitTests.Features.GuestTests.InviteGuestTest;

public class InviteGuestHandlerTest
{
    private readonly IUnitOfWork _unitOfWork = new UnitOfWork();

    [Fact]
    public async void CreateEvent_CreateGuest_InviteTheGuest_ReturnSuccess()
    {
        // Arrange
        var viaEvent = ViaEventTestFactory.ReadyEvent();
        var guest = GuestFactory.CreateGuest();
        var eventRepo = new EventRepository();
        eventRepo.Add(viaEvent);
        
        InviteGuestCommand inviteGuestCommand = InviteGuestCommand.Create(viaEvent._eventId.Value.ToString(), guest._Id.Value.ToString()).Payload;
        InviteGuestHandler handler = new(eventRepo, _unitOfWork);
        
        // Act
        var result = await  handler.Handle(inviteGuestCommand);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.True(viaEvent._Invitations.Any(invitation => invitation._GuestId.Value == guest._Id.Value));

        
    }
}