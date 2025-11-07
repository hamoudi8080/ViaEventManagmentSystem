using ViaEventManagementSystem.Core.AppEntry.Commands;
using ViaEventManagementSystem.Core.AppEntry.Commands.Event;
using ViaEventManagementSystem.Core.Domain.Aggregates.Events;
using ViaEventManagementSystem.Core.Domain.Common.UnitOfWork;
using ViaEventManagementSystem.Core.Tools.OperationResult;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagementSystem.Core.Application.CommandHandlers.Features.Event;

public class InviteGuestHandler : ICommandHandler<InviteGuestCommand>
{
    private readonly IViaEventRepository _eventRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public InviteGuestHandler(IViaEventRepository eventRepository, IUnitOfWork unitOfWork)
        => (_eventRepository, _unitOfWork) = (eventRepository, unitOfWork);
    
    
    public async Task<Result> Handle(InviteGuestCommand command)
    {
        var viaEvent = await _eventRepository.GetById(command.EventId);

        if (viaEvent == null)
        {
            return Result.Failure(Error.NotFound(ErrorMessage.General.EventNotFound));
        }

        Result inviteGuestResult = viaEvent.InviteGuest(command.GuestId);

        if (inviteGuestResult.IsSuccess)
        {
            await _unitOfWork.SaveChangesAsync();
        }

        return inviteGuestResult;
    }
}