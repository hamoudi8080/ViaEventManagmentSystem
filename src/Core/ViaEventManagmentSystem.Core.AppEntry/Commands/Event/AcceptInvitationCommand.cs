using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.Entities.ValueObjects;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Guests.ValueObjects;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagmentSystem.Core.AppEntry.Commands.Event;

public class AcceptInvitationCommand : ICommand
{
    public EventId EventId { get; init; }


    public GuestId GuestId { get; init; }


    private AcceptInvitationCommand(EventId eventId, GuestId id)
    {
        EventId = eventId;
        GuestId = id;
    }

    
    public static Result<AcceptInvitationCommand> Create(string eventId, string guestId) {
        //Todo: ask about parameter type of eventId and guestId in this create method. they are string, does it have to be object of fx eventId AND guestId? if so how to make it?

        Result<EventId> eventIdResult = EventId.Create(eventId);
        Result<GuestId> myguestId = GuestId.Create(guestId);

        if (eventIdResult.IsSuccess && myguestId.IsSuccess) {
            return Result<AcceptInvitationCommand>.Success(new AcceptInvitationCommand(eventIdResult.Payload!, myguestId.Payload!));
        }

        return Result<AcceptInvitationCommand>.Failure(Error.AddCustomError("Failed to create AcceptInvitationCommand due to invalid EventId or guestId"));
    }
    

    
    /*
    public static Result<AcceptInvitationCommand> Create(EventId eventId, GuestId guestId)
    {
        //ToDO: otherwise i will do like this, because if any error happend in creating EventId or GuestId, it will be handled in their classes. and when i pass them into this method's parameter. it will be null. then i just check if not null then..... if null throw error.
        if (eventId != null && guestId != null)
        {
            return Result<AcceptInvitationCommand>.Success(new AcceptInvitationCommand(eventId, guestId));
        }
        
        return Result<AcceptInvitationCommand>.Failure(
            Error.AddCustomError("Failed to create AcceptInvitationCommand due to invalid EventId or guestId"));
    }
    */
}