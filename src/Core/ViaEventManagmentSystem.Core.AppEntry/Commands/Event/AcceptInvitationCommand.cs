using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.Entities.ValueObjects;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagmentSystem.Core.AppEntry.Commands.Event;

public class AcceptInvitationCommand : ICommand
{
    public EventId EventId { get; init; }
    public InvitationId InvitationId { get; init; }
    
    private AcceptInvitationCommand(EventId eventId, InvitationId invitationId) {
        EventId = eventId;
        InvitationId = invitationId;
    }
    
    
    public static Result<AcceptInvitationCommand> Create(string eventId, string invitationId) {
        Result<EventId> eventIdResult = EventId.Create(eventId);
        Result<InvitationId> invitationIdResult = InvitationId.Create(invitationId);
        
        if (eventIdResult.IsSuccess && invitationIdResult.IsSuccess) {
            return Result<AcceptInvitationCommand>.Success(new AcceptInvitationCommand(eventIdResult.Payload!, invitationIdResult.Payload!));
        }
        
        return Result<AcceptInvitationCommand>.Failure(Error.AddCustomError("Failed to create AcceptInvitationCommand due to invalid EventId or InvitationId"));
    }
}