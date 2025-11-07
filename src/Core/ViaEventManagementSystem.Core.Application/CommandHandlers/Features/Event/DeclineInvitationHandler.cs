using ViaEventManagementSystem.Core.AppEntry.Commands;
using ViaEventManagementSystem.Core.AppEntry.Commands.Event;
using ViaEventManagementSystem.Core.Domain.Aggregates.Events;
using ViaEventManagementSystem.Core.Domain.Common.UnitOfWork;
using ViaEventManagementSystem.Core.Tools.OperationResult;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagementSystem.Core.Application.CommandHandlers.Features.Event;

public class DeclineInvitationHandler : ICommandHandler<DeclineInvitationCommand>
{
    private IViaEventRepository _eventRepository;
    private IUnitOfWork _unitOfWork;
    
    public DeclineInvitationHandler(IViaEventRepository eventRepository, IUnitOfWork unitOfWork)
        => (_eventRepository, _unitOfWork) = (eventRepository, unitOfWork);
    
    public async Task<Result> Handle(DeclineInvitationCommand command)
    {
        var _ViaEvent = await _eventRepository.GetById(command.EventId);

        if (_ViaEvent == null)
        {
            return Result.Failure(Error.NotFound(ErrorMessage.General.EventNotFound));
        }

        Result eventDeclineResult = _ViaEvent.RejectGuestInvitation(command.GuestId);

        if (eventDeclineResult.IsSuccess)
        {
            await _unitOfWork.SaveChangesAsync();
        }

        return eventDeclineResult;
    }
}